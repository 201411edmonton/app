require 'albacore'

namespace :build do
  desc 'compiles the libs'
  task :compile => %w/build:_compile build:_compile_all/ 

  csc :_compile => :init do|csc| 
    csc.compile FileList["source/**/*.cs"].exclude("AssemblyInfo.cs").exclude("source/app.web.ui/**/*.cs")
    csc.references configatron.all_references
    csc.output = File.join(configatron.artifacts_dir,"#{configatron.project}.dll")
    csc.target = :library
  end

  csc :_compile_all => :init do|csc| 
    csc.compile FileList["source/**/*.cs"].exclude("AssemblyInfo.cs")
    csc.references configatron.all_references
    csc.output = File.join(configatron.artifacts_dir,"#{configatron.project}_specs.dll")
    csc.target = :library
  end

  desc 'compiles the web project'
  aspnetcompiler :_web => [:init, :copy_config_files] do |c|
    c.physical_path = "source/app.web.ui"
    c.target_path = configatron.web_staging_dir
    c.updateable = true
    c.force = true
  end

  task :web => [:init, :copy_config_files,"build:compile"] do |c|
    FileUtils.rm_rf "source/app.web.ui/Bin"
    FileUtils.mkdir "source/app.web.ui/Bin"
    FileUtils.cp File.join(configatron.artifacts_dir,"#{configatron.project}.dll"), "source/app.web.ui/Bin"
    Rake::Task["build:_web"].invoke
  end

  task :rebuild => ["clean","compile"]

  desc 'run the web application'
  task :run => [:kill_iis,'build:web'] do
    system("start start_web_app.bat")
    system("start #{configatron.browser} #{configatron.start_url}")
  end
end


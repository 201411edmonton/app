namespace :build do
  desc 'compiles the project'
  csc :compile, [:compile_file] => :init do|csc| 
    load options[:compile_file]
    project = settings.compile_unit
    csc.compile project.sources
    csc.references project.all_references
    csc.output = project.output
    csc.target = project.target
  end

  desc 'compiles a web project'
  aspnetcompiler :_web => :init do |c|
    c.physical_path = project.source_path
    c.target_path = project.target_path
    c.updateable = true
    c.force = true
    # c.physical_path = "source/app.web.ui"
    # c.target_path = configatron.web_staging_dir
    # c.updateable = true
    # c.force = true
  end

  task :web => [:init, :copy_config_files,"build:compile"] do |c|
    FileUtils.rm_rf "source/app.web.ui/Bin"
    FileUtils.mkdir "source/app.web.ui/Bin"
    FileUtils.cp File.join(configatron.artifacts_dir,"#{configatron.project}.dll"), "source/app.web.ui/Bin"
    Rake::Task["build:_web"].invoke
  end
    # project = settings.compile_unit
    # csc.compile project.sources
    # csc.references project.all_references
    # csc.output = File.join(configatron.artifacts_dir,"#{configatron.project}.specs.dll")
    # csc.target = :library

end

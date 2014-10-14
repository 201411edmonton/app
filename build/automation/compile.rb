module Automation
  class Compile < Thor
    namespace :compile

    desc 'rebuild', 'rebuilds the project'
    def rebuild
      invoke 'automation:init'

      Dir.glob("compile_units/*.compile").each do |file|
        invoke :project, [file]
      end
    end

    desc 'project', 'compiles a project'
    def project(compile_file)
      ::Automation::General.new.init
      require_rake

      load compile_file
      Rake::Task['build:compile'].invoke
    end

    desc 'web', 'compiles a web project'
    def web(web_file)
      ::Automation::General.new.init
      require_rake

      load web_file
      web_project = settings.web_unit

      FileUtils.rm_rf web_project.bin_folder
      FileUtils.mkdir_p web_project.bin_folder

      web_project.dependent_compiles.each do |file|
        unit = settings.compile_unit
        project(file)
        FileUtils.cp unit.output, "source/app.web.ui/Bin"
      end

      Rake::Task['build:web'].invoke
    end

    no_commands do
      def require_rake
        require 'rake'
        require 'albacore'
        require_relative '../legacy_tasks/build'
      end
    end
  end
end

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
      project = settings.compile_unit

      csc = CSC.new
      csc.compile project.sources
      csc.references project.references
      csc.output = project.output
      csc.target = project.target
      csc.execute
    end

    desc 'web', 'compiles a web project'
    def web(web_file)
      ::Automation::General.new.init
      require_rake

      load web_file
      project = settings.web_unit

      FileUtils.rm_rf project.bin_folder
      FileUtils.mkdir_p project.bin_folder

      project.dependent_compiles.each do |file|
        unit = settings.compile_unit
        project(file)
        FileUtils.cp unit.output, "source/app.web.ui/Bin"
      end

      asp_net_compiler = AspNetCompiler.new

      asp_net_compiler.instance_eval do |c|
        c.physical_path = project.physical_path
        c.target_path = project.target_path
        c.updateable = true
        c.force = true
        c.execute
      end
    end

    no_commands do
      def require_rake
        require 'rake'
        require 'albacore'
      end
    end
  end
end

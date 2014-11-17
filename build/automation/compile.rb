require 'albacore'

module Automation
  class Compile < Thor
    include ::Automation::InputUtils
    namespace :compile

    desc 'rebuild', 'rebuilds the project'
    def rebuild
      invoke 'automation:init'

      settings.build_order.each do |file|
        project file
      end
    end

    desc 'selection', 'compiles the selected project'
    def selection
      compile_unit = pick_item_from(settings.compile_units, "Pick a compile unit to run")
      project compile_unit
    end

    desc 'project', 'compiles a project'
    def project(compile_file)
      ::Automation::General.new.init

      load compile_file
      project = settings.compile_unit
      compile_dependent_projects project

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

      load web_file
      project = settings.web_unit

      FileUtils.rm_rf project.bin_folder
      FileUtils.mkdir_p project.bin_folder

      compile_dependent_projects project

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
      def compile_dependent_projects(project)
        project.dependent_compiles.each do |file|
          load file
          unit = settings.compile_unit
          project(file)
          FileUtils.cp unit.output, settings.web.vs_output_dir
        end
      end
    end
  end
end

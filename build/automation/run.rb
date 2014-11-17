module Automation
  class Run < Thor
    include ::Automation::InputUtils
    namespace :run

    method_option :compile_file, default: nil

    desc 'unit', 'Running a compile unit'
    def unit
      compile_unit = options[:compile_file] || pick_item_from(settings.compile_units, "Pick a compile unit to run")
      load compile_unit
      project = settings.compile_unit
      invoke 'compile:project', [compile_unit]
      system("#{project.output}")
    end

    desc 'web', 'Running a web based app'
    def web(compile_file)
      invoke 'processes:kill_runner_processes', []
      invoke 'compile:web'
      invoke 'automation:expand', []

      system("start start_web_app.bat")
      system("start #{settings.browser} #{settings.web.start_url}")
    end
  end
end

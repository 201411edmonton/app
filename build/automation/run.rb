module Automation
  class Run < Thor
    namespace :run

    desc 'web', 'Running a web based app'
    def web(compile_file)
      system('kill_runner_processes.bat')
      invoke 'compile:web'
      invoke 'automation:expand', []
      invoke 'config:copy_files', []

      system("start start_web_app.bat")
      system("start #{settings.browser} #{settings.start_url}")
    end
  end
end

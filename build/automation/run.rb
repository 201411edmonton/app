module Automation
  class Run < Thor
    namespace :run

    desc 'web', 'Running a web based app'
    def web(compile_file)
      system('kill_runner_processes.bat')
      invoke 'compile:web'
      ::Automation::General.new.expand
      system("start start_web_app.bat")
      system("start #{configatron.browser} #{configatron.start_url}")
    end
  end
end

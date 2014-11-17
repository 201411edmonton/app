module Automation
  class Processes < Thor
    namespace :processes

    desc 'kill_runner_processes', 'kill all the runner processes'
    def kill_runner_processes
      settings.runner_processes.each do |process|
        system("taskkill /F /IM #{process}.exe")
      end
    end
  end
end

module Automation
  class Growl < Thor
    namespace :growl

    desc 'start', 'starts the growl runner'
    def start
      system("start build/tools/growl/Growl.exe")
    end
  end
end

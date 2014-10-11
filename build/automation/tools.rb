module Automation
  class Tools < Thor
    namespace :tools

    desc 'start_growl', 'Start the growl monitor'
    def start_growl
      system("start build/tools/growl/Growl.exe")
    end
  end
end

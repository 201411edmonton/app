module Automation
  class General < Thor
    namespace :automation

    desc 'init', 'kick off task'
    def init
      invoke :clean
      settings.automation.folders_to_create.each do |folder|
        FileUtils.mkdir_p folder if ! File.exists?(folder)
      end
    end

    desc 'clean', 'cleans out old files'
    def clean
      settings.automation.folders_to_clean.each do |folder|
        FileUtils.rm_rf folder 
      end
    end

    desc 'expand', 'Expand templates'
    def expand
      Expansion::CLIInterface.run("ExpansionFile")
    end
  end
end

require_relative 'automation/git_utils'
require_relative 'automation/compile'
require_relative 'automation/configuration'
require_relative 'automation/git'
require_relative 'automation/tools'
require_relative 'automation/continuous_testing'
require_relative 'automation/run'
require_relative 'automation/processes'
require_relative 'automation/specs'

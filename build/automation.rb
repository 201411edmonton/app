module Automation
  class General < Thor
    namespace :automation

    desc 'init', 'kick off task'
    def init
      invoke 'automation:clean'
    end

    desc 'clean', 'cleans out old files'
    def clean
      [
        settings.artifacts_dir,
        settings.specs.dir,
        settings.specs.report_dir,
      ].each do |folder| 
        FileUtils.mkdir_p folder if ! File.exists?(folder)
      end
    end
  end

end

require_relative 'automation/git_utils'
require_relative 'automation/compile'
require_relative 'automation/configuration'
require_relative 'automation/git'
require_relative 'automation/tools'
require_relative 'automation/continuous_testing'
require_relative 'automation/specs'

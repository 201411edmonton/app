module Automation
  class Startup < Thor
    include ::Automation::GitUtils

    namespace :startup

    desc 'configure', 'configure startup repos'
    def configure
      git <<command
remote rm jp
remote add jp #{settings.git.provider}:#{settings.git.user}/#{settings.git.repo}.git
command

      create_first_branches
    end

    no_commands do
      def create_first_branches
        %w/clean master starting_point/.each do |branch| 
          checkout(branch)
        end
      end
    end
  end
end

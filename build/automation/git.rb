module Automation
  class Git < Thor
    include ::Automation::GitUtils
    namespace :git

    desc 'student_remotes', 'set up the remotes for git'
    def student_remotes
      settings.git.remotes.each do|remote|
        git <<command
remote rm #{remote}
remote add #{remote} #{settings.git.provider}:#{remote}/#{settings.git.repo}
command
      end
    end

    desc 'get_latest_from_jp', 'get the latest code from jp'
    def get_latest_from_jp
      exit_if_on_branches(%w/master clean/)

      git <<command
add -A
commit -m "Committing"
pull jp master
command
    end

    method_option :remote_name, default: nil
    desc 'fetch_branch', 'fetches the branch from a remote'
    def fetch_branch
      exit_if_on_branches(%w/master clean/)

      remote_name = options[:remote_name] || choose_remote(get_all_available_non_origin_remotes)

      branch = get_specific_remote_branch_name_from(remote_name) unless remote_name.empty?
      get_specific_branch_on(remote_name, branch)
    end

    method_option :remote_name, default: nil
    desc 'fetch_latest', 'fetch the latest branch from a remote'
    def fetch_latest
      remote_name = options[:remote_name] || choose_remote(get_all_available_non_origin_remotes) if remote_name == nil
      unless remote_name.empty?
        branch = get_latest_remote_branch_name(remote_name)
        get_specific_branch_on(remote_name, branch)
      end
    end

    desc 'trash', 'trash the current work'
    def trash
      git <<command
reset --hard
clean -df
command
    end
  end
end

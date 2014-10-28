module Automation
  class Work < Thor
    include ::Automation::GitUtils

    namespace :work

    desc 'save_my_changes', 'push your changes to github'
    def save_my_changes
      exit_if_on_branches(%w/master clean/)

      git <<command
add -A
commit -m "Pushing new changes"
push origin
command
    end

    desc 'start_new_exercise', 'saves your current changes and pulls the latest work from presenter'
    def start_new_exercise
      exit_if_on_branches(%w/master clean/)

      current_time = Time.now
      new_branch_name = current_time.strftime("%Y%m%d%H%M%S%3N")
      git <<command
add -A
commit -m "Committing"
command

      branch_name = ask("Enter a meaningful branch name (leave empty if you don't need one)") || ""
      branch_name = branch_name.strip.gsub(" ","_").downcase

      checkout(branch_name) unless branch_name == ""
      checkout('clean')

      exit_if_not_on_the_branch 'clean'

      checkout(new_branch_name)
      git "pull jp master"
      puts "new branch name:#{new_branch_name}"
    end
  end
end

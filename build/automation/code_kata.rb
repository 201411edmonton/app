module Automation
  class CodeKata < Thor
    include ::Automation::GitUtils

    namespace :code_kata


    desc 'start', 'start a code kata'
    def start
      exit_if_on_branches(%w/master clean/)

      git <<command
add -A
commit -m 'Committing'
command

      checkout('codekata')
    end

  end
end

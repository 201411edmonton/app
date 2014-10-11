module Automation
  module GitUtils
    def exit_if_on_the_branch(branch)
      exit_if_branch_condition_fails(lambda{|expression,status|expression =~ status},branch,lambda{|item|  "You cant run this command on the branch #{item}"})
    end

    def exit_if_not_on_the_branch(branch)
      exit_if_branch_condition_fails(lambda{|expression,status|expression !~ status},branch,lambda{|item| "You need to run this on the branch #{item}"})
    end

    def exit_if_branch_condition_fails(specification_block,branch,message_block)
      status = run_git_command("branch",true)
      match_expression = %r/\* #{branch}/

      if (specification_block.call(match_expression,status))
        puts message_block.call(branch)
        exit
      end
    end
    
    def git(here_doc)
      here_doc.split('\n').each do |line|
        run_git_command(line)
      end
    end

    def run_git_command(command,capture_ouptut = false)
      full_command = "git #{command}"
      if (capture_ouptut)
        `#{full_command}`
      else
        system(full_command)
      end
    end

    def checkout(branch_name)
      git <<command
checkout -b #{branch_name}
checkout #{branch_name}
command
    end


    def pick_item_from(items,prompt)
      items.each_with_index{|item,index| p "#{index + 1} - #{item}"}
      p prompt
      index = gets.chomp.to_i
      return index == 0 ? "": items[index-1]
    end

    def get_specific_remote_branch_name_from(remote_name)
      git "fetch #{remote_name}"
      branches = run_git_command("branch --remote",true)
      branches = branches.split("\n")
      branches = branches.select do |item| 
        %r/#{remote_name}/ =~ item
      end
      branches = branches.map do |item| 
        item.gsub(%r/#{remote_name}\//,"").strip
      end
      pick_item_from(branches,"Choose Branch:")
    end

    def get_all_available_non_origin_remotes
      remotes = run_git_command("remote",true)
      remotes = remotes.split("\n")
      remotes = remotes.select do |remote| 
        /origin/ !~ remote
      end
    end

    def choose_remote(remotes)
      pick_item_from(remotes,"Choose remote:")
    end
  end

  def get_latest_remote_branch_name(remote_name)
    branch_pattern = /^\d*$/
    branches = run_git_command("fetch #{remote_name}")
    ref_name = File.join('.git', 'refs', 'remotes', remote_name)
    latest_branch = Dir.entries(ref_name)
    latest_branch = latest_branch.select{|folder| branch_pattern =~ folder}
    latest_branch = latest_branch.sort{|first,second| second <=> first}.first
  end

  def update_to_latest_branch_on(remote_name)
    git <<command
add -A
commit -m 'Updated'
checkout clean
command

    latest_branch = get_latest_remote_branch_name(remote_name)

    new_branch = "pull_#{remote_name}_#{latest_branch}"

    git <<command
checkout -b #{new_branch}
checkout #{new_branch}
pull #{remote_name} #{latest_branch}
command
  end
end

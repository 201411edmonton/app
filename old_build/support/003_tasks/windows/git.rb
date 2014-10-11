namespace :git do
  desc 'set up the remotes for git'
  task :student_remotes do
    puts configatron.git.repo
    configatron.git.remotes.each do|remote|
      `git remote rm #{remote}`
      `git remote add #{remote} #{configatron.git.provider}:#{remote}/#{configatron.git.repo}`
    end
  end

end

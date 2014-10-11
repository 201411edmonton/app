module Automation
  class Compile < Thor
    namespace :compile

    desc 'rebuild', 'rebuilds the project'
    def rebuild
      `rake build:rebuild`
    end
  end
end

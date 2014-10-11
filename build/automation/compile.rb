module Automation
  class Compile < Thor
    namespace :compile

    desc 'rebuild', 'rebuilds the project'
    def rebuild
      `rake clean`
      Dir.glob("*.compile").each do |file|
        `rake build:compile[#{file}]`
      end
    end
  end
end

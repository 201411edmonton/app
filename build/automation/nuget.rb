module Automation
  class Nuget < Thor
    namespace :nuget

    desc 'install', 'installs the required third party libs'
    def install
      system("nuget install -OutputDirector #{settings.nuget.packages_folder} #{settings.nuget.packages_config}")
    end
  end
end

module Automation
  class Configuration < Thor
    namespace :config

    desc 'copy_files', 'copies configuration files to all runtime locations'
    def copy_files
      config_files = Dir.glob("source/config/*.erb")

      config_files.each do |file|
          [settings.artifacts_dir,settings.app_dir].each do|folder|
            FileUtils.cp(file.name_without_template_extension,
            folder.join(file.base_name_without_extension))
          end
      end
    end
  end

end

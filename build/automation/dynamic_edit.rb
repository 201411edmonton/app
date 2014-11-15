module Automation
  class DynamicEdit < Thor
    include ::Automation::InputUtils
    namespace :edit

    PROJECT_TEMPLATE = 'edit.csproj.erb'

    method_option :compile_file, default: nil

    desc 'edit', 'edit files in a compile unit'
    def edit
      view = options[:compile_file] || pick_item_from(settings.compile_units, "Pick a compile unit to edit")
      FileUtils.cp 'edit.csproj.template', PROJECT_TEMPLATE
      load view
      Expansion::CLIInterface.run("ExpansionFile")
      FileUtils.rm PROJECT_TEMPLATE
    end
  end
end

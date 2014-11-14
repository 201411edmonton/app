module Automation
  class DynamicEdit < Thor
    include ::Automation::InputUtils
    namespace :edit

    method_option :compile_file, default: nil
    desc 'edit', 'edit files in a compile unit'
    def edit
      view = options[:compile_file] || pick_item_from(settings.compile_units, "Pick a compile unit to edit")
      load view
      Expansion::CLIInterface.run("ExpansionFile")
    end

  end
end

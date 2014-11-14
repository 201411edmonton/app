module Automation
  module InputUtils
    def pick_item_from(items,prompt)
      items.each_with_index{|item,index| puts "#{index + 1} - #{item}"}
      index = ask(prompt).to_i
      return index == 0 ? "": items[index-1]
    end
  end
end

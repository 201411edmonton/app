module Automation
  class Specs < Thor
    namespace :specs

    desc 'view', 'view the spec report'
    def view 
      system "start #{settings.specs.report_dir}/#{settings.project}.specs.html"
    end

    desc 'ct', 'start the continuous tester'
    def continous_test
      invoke 'tools:start_growl'
      invoke 'continuous_testing:run_it'
    end

    desc 'run_them','run the specs for the project'
    def run_them
      invoke 'automation:init'
      invoke 'compile:rebuild'

      settings.all_references.each do|file|
        FileUtils.cp(file,settings.artifacts_dir)
      end

      spec_options = {
        exe: "#{settings.artifacts_dir}/mspec-clr4.exe",
        html: "#{settings.specs.report_dir}/#{settings.project}.specs.html",
        x: "example",
      }

      line = build_runner_line(spec_options, settings.specs.assemblies)
      system(line)
    end

    no_commands do
      def build_runner_line(options, assemblies)
        exe = options.delete(:exe)

        lines = options.inject([]) do |args, (key, value)|
          args << "--#{key}=#{value}"
        end

        lines.unshift(exe)
        lines = lines.concat(assemblies)

        "./#{lines.join(' ')}"
      end
    end
  end
end

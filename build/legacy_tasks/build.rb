namespace :build do
  desc 'compiles the project'

  csc :compile do|csc| 
    project = settings.compile_unit
    csc.compile project.sources
    csc.references project.references
    csc.output = project.output
    csc.target = project.target
  end

  desc 'compiles a web project'
  aspnetcompiler :web do |c|
    project = settings.web_unit
    c.physical_path = project.physical_path
    c.target_path = project.target_path
    c.updateable = true
    c.force = true
  end
end

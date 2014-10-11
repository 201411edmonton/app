module Automation
  class DB < Thor
    namespace :db

    desc 'create_schema', 'tears down the database and recreates it from the ddl files'
    def create_schema
      invoke 'automation:init'
      sql_runner.process_sql_files(create_sql_fileset('ddl'))

      # sh "build/tools/sql_metal/SqlMetal.exe /server:#{configatron.server_name} /database:#{configatron.initial_catalog} /code:[Replace with path to code file] /namespace:[Replace with namespace for code file]"
    end

    desc 'load_data', 'loads the database with acceptance testing data'
    def load_data
      invoke :create_schema

      # sql_runner.process_sql_files(create_sql_fileset('data'))
    end
  end
end

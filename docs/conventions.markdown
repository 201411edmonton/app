#Project Conventions

1. Test Naming Convention
  * As specified before

2. Convention for configuration
  * All micro configuration files will live under the source/config folder.
  * Files will be named:
    [FILE_NAME].[extension].erb

    ex. my_config.txt.erb

3. All components with dependencies will be guaranteed to have their dependencies provided upon construction by a factory component.

4. Null not an allowed return value, you must use either:

   1. Exceptions
   2. Null Object/Special Case

5. All requests will have at most one logical handler that can process it.

6. All handlers that require input will accept that input using a formal input model.

7. We are using a startup pipeline to handle startup activities:
  * All steps in the startup pipeline must conform to the contract IRunAStartupStep. They must also provide a single constructor that takes the startup pipeline configuration service.

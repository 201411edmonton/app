using System.Collections.Generic;
using app.containers.basic;
using app.containers.core;

namespace app.startup.steps
{
  public class ConfigureTheContainer : IRunAStartupStep
  {
    IHelpConfigureStartupPipelines startup_configuration;

    public ConfigureTheContainer(IHelpConfigureStartupPipelines startup_configuration)
    {
      this.startup_configuration = startup_configuration;
    }

    public void run()
    {
      var factories = new List<ICreateADependency>();

      IGetFactoriesForDependencies factory_registry = new DependencyFactories(factories,
        stubs.DelegateStubs.create_missing_dependency_factory);

      var container = new BasicContainer(factory_registry, stubs.DelegateStubs.create_exception_for_failure_to_create_dependency);

      Dependencies.configure_the_container = () => container;
    }
  }
}
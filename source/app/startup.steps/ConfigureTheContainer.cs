using app.containers.basic;
using app.containers.core;
using app.startup.stubs;

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
      var factories = new DependencyRegistration();

      IGetFactoriesForDependencies factory_registry = new DependencyFactories(factories,
        DelegateStubs.create_missing_dependency_factory);

      var container = new BasicContainer(factory_registry,
        DelegateStubs.create_exception_for_failure_to_create_dependency);

      factories.register<IRegisterDependencyFactories>(factories);
      factories.register<IFetchDependencies>(container);

      Dependencies.configure_the_container = () => container;
    }
  }
}
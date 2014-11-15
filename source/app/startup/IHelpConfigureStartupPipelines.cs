using app.containers.core;

namespace app.startup
{
  public interface IHelpConfigureStartupPipelines
  {
    IRegisterDependencyFactories component_registration { get; }
  }
}
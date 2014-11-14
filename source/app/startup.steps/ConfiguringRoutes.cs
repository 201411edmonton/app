namespace app.startup.steps
{
  public class ConfiguringRoutes : IRunAStartupStep
  {
    IHelpConfigureStartupPipelines startup_config;

    public ConfiguringRoutes(IHelpConfigureStartupPipelines startup_config)
    {
      this.startup_config = startup_config;
    }

    public void run()
    {
    }
  }
}
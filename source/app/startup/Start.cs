using app.startup.stubs;

namespace app.startup
{
  public class Start
  {
    public static ICreateAStartupPipelineBuilder create_pipeline_builder = () =>
    {
      return new StartupPipelineBuilder(new NonStep(), DelegateStubs.create_startup_step(),
        core.stubs.DelegateStubs.combine_actions);
    };

    public static ISpecifyTheFirstStepInAStartupPipeline by
    {
      get { return create_pipeline_builder(); }
    }
  }
}
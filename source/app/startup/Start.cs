using app.core.stubs;

namespace app.startup
{
  public class Start
  {
    public static ICreateAStartupPipelineBuilder create_pipeline_builder = () =>
    {
      return new StartupPipelineBuilder(new NonStep(), null,
        DelegateStubs.combine_actions);
    };

    public static ISpecifyTheFirstStepInAStartupPipeline by
    {
      get { return create_pipeline_builder(); }
    }
  }
}
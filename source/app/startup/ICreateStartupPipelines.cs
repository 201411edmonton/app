namespace app.startup
{
  public interface ISpecifyTheFirstStepInAStartupPipeline
  {
    IAddExtraStepsToAStartupPipeline running<Step>() where Step : IRunAStartupStep;
  }

  public interface IAddExtraStepsToAStartupPipeline
  {
    IAddExtraStepsToAStartupPipeline then<Step>() where Step : IRunAStartupStep;
    void finish_with<Step>() where Step : IRunAStartupStep;
  }
}
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace app.startup
{
  [Subject(typeof(Start))]
  public class StartSpecs
  {
    public abstract class concern : Observes
    {
    }

    public class when_accessing_the_startup_pipleine_builder : concern
    {
      Establish c = () =>
      {
        startup_pipeline_builder = fake.an<ISpecifyTheFirstStepInAStartupPipeline>();
        ICreateAStartupPipelineBuilder builder_factory = () => startup_pipeline_builder;

        spec.change(() => Start.create_pipeline_builder).to(builder_factory);
      };

      Because b = () =>
        result = Start.by;

      It provides_access_to_the_builder_created_by_the_factory_for_the_builder = () =>
        result.ShouldEqual(startup_pipeline_builder);

      static ISpecifyTheFirstStepInAStartupPipeline result;
      static ISpecifyTheFirstStepInAStartupPipeline startup_pipeline_builder;
    }
  }
}
using System;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace app.containers.core
{
  [Subject(typeof(Dependencies))]
  public class DependenciesSpecs
  {
    public abstract class concern : Observes
    {
    }

    public class when_providing_access_to_the_container_facade : concern
    {
      Establish c = () =>
      {
        the_container_facade = fake.an<IFetchDependencies>();
        IConfigureTheContainerFacade configuration = () => the_container_facade;

        spec.change(() => Dependencies.configure_the_container).to(configuration);
      };

      Because b = () =>
        result = Dependencies.fetch;

      It returns_the_facade_configured_during_the_startup_pipeline = () =>
        result.ShouldEqual(the_container_facade);

      static IFetchDependencies result;
      static IFetchDependencies the_container_facade;
    }
  }

  public class Dependencies
  {
    public static IConfigureTheContainerFacade configure_the_container = delegate
    {
      throw new NotImplementedException("This needs to be configured by a starup pipeline");
    };

    public static IFetchDependencies fetch
    {
      get
      {
        return configure_the_container();
      }
    }
  }
}
using System;
using System.Data;
using app.containers.core;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace app.containers.basic
{
  [Subject(typeof(BasicContainer))]
  public class BasicContainerSpecs
  {
    public abstract class concern : Observes<IFetchDependencies,
      BasicContainer>
    {
    }

    public class when_getting_a_dependency : concern
    {
      Establish c = () =>
      {
        command = fake.an<IDbCommand>();
        factory = fake.an<ICreateADependency>();
        factories = depends.on<IGetFactoriesForDependencies>();

        factories.setup(x => x.get_factory_that_can_create(typeof(IDbCommand)))
          .Return(factory);

        factory.setup(x => x.create()).Return(command);
      };

      Because b = () =>
        result = sut.an<IDbCommand>();

      It returns_the_item_created_by_the_factory_for_that_dependency = () =>
        result.ShouldEqual(command);

      static IDbCommand result;
      static IDbCommand command;
      static ICreateADependency factory;
      static IGetFactoriesForDependencies factories;
    }
  }

  public interface IGetFactoriesForDependencies
  {
    ICreateADependency get_factory_that_can_create(Type type);
  }

  public interface ICreateADependency
  {
    object create();
  }

  public class BasicContainer : IFetchDependencies
  {
    IGetFactoriesForDependencies factories;

    public BasicContainer(IGetFactoriesForDependencies factories)
    {
      this.factories = factories;
    }

    public Dependency an<Dependency>()
    {
      throw new NotImplementedException();
    }
  }
}
using System;
using System.Data;
using app.containers.core;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;
using Rhino.Mocks.Constraints;

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
      public class and_the_factory_for_the_dependency_can_create_it_successfully
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

      public class and_the_factory_for_the_dependency_fails_to_create_the_dependency
      {
        Establish c = () =>
        {
          original_exception = new Exception();
          custom_exception = new Exception();

          factory = fake.an<ICreateADependency>();
          factories = depends.on<IGetFactoriesForDependencies>();

          depends.on<ICreateAnExceptionWhenTheDependencyCantBeCreated>((type, exception) =>
          {
            type.ShouldEqual(typeof(IDbCommand));
            exception.ShouldEqual(original_exception);
            return custom_exception;
          });

          factories.setup(x => x.get_factory_that_can_create(typeof(IDbCommand)))
            .Return(factory);

          factory.setup(x => x.create()).Throw(original_exception);
        };

        Because b = () =>
          spec.catch_exception(() => sut.an<IDbCommand>());

        It throws_the_exception_created_by_the_dependency_creation_exception_factory = () =>
          spec.exception_thrown.ShouldEqual(custom_exception);

        static ICreateADependency factory;
        static IGetFactoriesForDependencies factories;
        static Exception custom_exception;
        static Exception original_exception;
      }

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
        return (Dependency)factories.get_factory_that_can_create(typeof (Dependency)).create();
    }
  }
}
using System;
using System.Collections.Generic;
using System.Linq;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace app.containers.basic
{
  [Subject(typeof(DependencyFactories))]
  public class DependencyFactoriesSpecs
  {
    public abstract class concern : Observes<IGetFactoriesForDependencies,
      DependencyFactories>
    {
    }

    public class when_getting_a_factory_for_a_dependency : concern
    {
      public class and_it_has_the_factory
      {
        Establish c = () =>
        {
          factory_that_can_create_dependency = fake.an<ICreateADependency>();
          all_factories = Enumerable.Range(1, 100).Select(x => fake.an<ICreateADependency>()).ToList();

          all_factories.Add(factory_that_can_create_dependency);

          factory_that_can_create_dependency.setup(x => x.can_create(typeof(MyDependency)))
            .Return(true);

          depends.on<IEnumerable<ICreateADependency>>(all_factories);
        };

        Because b = () =>
          result = sut.get_factory_that_can_create(typeof(MyDependency));

        It returns_the_factory = () =>
          result.ShouldEqual(factory_that_can_create_dependency);

        static ICreateADependency result;
        static ICreateADependency factory_that_can_create_dependency;
        static List<ICreateADependency> all_factories;
      }
    }

    public class MyDependency
    {
    }
  }

  public class DependencyFactories : IGetFactoriesForDependencies
  {
    public ICreateADependency get_factory_that_can_create(Type type)
    {
      throw new NotImplementedException();
    }
  }
}
﻿using System;
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

      public class and_it_does_not_have_the_factory
      {
        //Arrange
        Establish c = () =>
        {
          special_case = fake.an<ICreateADependency>();
          all_factories = Enumerable.Range(1, 100).Select(x => fake.an<ICreateADependency>()).ToList();

          depends.on<IEnumerable<ICreateADependency>>(all_factories);
          depends.on<ICreateADependencyFactoryWhenOneCantBeFound>(x =>
          {
            x.ShouldEqual(typeof(MyDependency));
            return special_case;
          });
        };

        Because b = () =>
          result = sut.get_factory_that_can_create(typeof(MyDependency));

        It returns_the_special_case = () =>
          result.ShouldEqual(special_case);

        static ICreateADependency result;
        static List<ICreateADependency> all_factories;
        static ICreateADependency special_case;
      }
    }

    public class MyDependency
    {
    }
  }

  public class DependencyFactories : IGetFactoriesForDependencies
  {
    IEnumerable<ICreateADependency> factories;
    ICreateADependencyFactoryWhenOneCantBeFound special_case_builder;

    public DependencyFactories(IEnumerable<ICreateADependency> factories, ICreateADependencyFactoryWhenOneCantBeFound special_case_builder)
    {
      this.factories = factories;
      this.special_case_builder = special_case_builder;
    }

    public ICreateADependency get_factory_that_can_create(Type type)
    {
      return factories.FirstOrDefault(x => x.can_create(type)) ?? special_case_builder(type);
    }
  }
}
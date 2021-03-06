﻿using System;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace app.containers.basic
{
  [Subject(typeof(BasicFactory))]
  public class BasicFactorySpecs
  {
    public abstract class concern : Observes<ICreateOneDependency,
      BasicFactory>
    {
    }

    public class when_creating_a_dependency : concern
    {
      Establish c = () =>
      {
        dependency = new DependencyFactoriesSpecs.MyDependency();
        depends.on<Func<object>>(() => dependency);
      };

      Because b = () =>
        result = sut.create();

      It return_the_item_created_by_the_provided_factory = () =>
        result.ShouldEqual(dependency);

      static object result;
      static DependencyFactoriesSpecs.MyDependency dependency;
    }
  }

  public class BasicFactory : ICreateOneDependency
  {
    Func<object> object_creator;

    public BasicFactory(Func<object> object_creator)
    {
      this.object_creator = object_creator;
    }

    public object create()
    {
      return object_creator();
    }
  }
}

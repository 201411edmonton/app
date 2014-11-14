 using System;
 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;

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

      It return_the_item_created_by_the_provided_block = () =>
        result.ShouldEqual(dependency);

      static object result;
      static DependencyFactoriesSpecs.MyDependency dependency;
    }
  }

  public class BasicFactory:ICreateOneDependency
  {
    public object create()
    {
      throw new System.NotImplementedException();
    }
  }
}

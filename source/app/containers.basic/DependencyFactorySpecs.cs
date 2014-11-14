 using System;
 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;

namespace app.containers.basic
{  
  [Subject(typeof(DependencyFactory))]  
  public class DependencyFactorySpecs
  {
    public abstract class concern : Observes<ICreateADependency,
      DependencyFactory>
    {
        
    }

   
    public class when_determining_if_it_can_create_a_dependency : concern
    {
      Establish c = () =>
      {
        depends.on<IMatchAType>(x => x == typeof(DependencyFactoriesSpecs.MyDependency));
      };

      Because b = () =>
        result = sut.can_create(typeof(DependencyFactoriesSpecs.MyDependency));

      It decides_by_using_its_type_specification = () =>
        result.ShouldBeTrue();

      static bool result;

        
    }
  }

  public class DependencyFactory : ICreateADependency
  {
      private IMatchAType type_matches;

      public DependencyFactory(IMatchAType type_matches)
      {
          this.type_matches = type_matches;
      }

      public object create()
    {
      throw new NotImplementedException();
    }

    public bool can_create(Type type)
    {
        return type_matches(type);
    }
  }
}

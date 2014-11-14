using System;
using app.containers.basic;

namespace app.startup.stubs
{
  public class DelegateStubs
  {
    public static ICreateAnExceptionWhenTheDependencyCantBeCreated create_exception_for_failure_to_create_dependency = (type_that_could_not_be_created, inner) =>
      new Exception(string.Format("Could not create the type {0}", type_that_could_not_be_created.FullName), inner);

    public static ICreateADependencyFactoryWhenOneCantBeFound create_missing_dependency_factory = x =>
    {
      throw new NotImplementedException(string.Format("There is no factory that can create a {0}", x.FullName));
    };

    public static IMatchAType is_type<DependencyType>()
    {
      return x => x == typeof(DependencyType); 
    }
  }
}
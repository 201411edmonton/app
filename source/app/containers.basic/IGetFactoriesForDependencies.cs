using System;

namespace app.containers.basic
{
  public interface IGetFactoriesForDependencies
  {
    ICreateADependency get_factory_that_can_create(Type type);
  }
}
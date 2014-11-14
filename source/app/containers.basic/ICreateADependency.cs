using System;

namespace app.containers.basic
{
  public interface ICreateADependency
  {
    object create();
    bool can_create(Type type);
  }

}
using System;

namespace app.containers.basic
{
  public delegate ICreateADependency ICreateADependencyFactoryWhenOneCantBeFound(Type type_that_has_no_factory);
}
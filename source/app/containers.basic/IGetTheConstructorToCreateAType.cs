using System;
using System.Reflection;

namespace app.containers.basic
{
  public delegate ConstructorInfo IGetTheConstructorToCreateAType(Type type);
}
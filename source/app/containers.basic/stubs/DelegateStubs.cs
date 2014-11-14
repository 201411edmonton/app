using System.Linq;

namespace app.containers.basic.stubs
{
  public class DelegateStubs
  {
    public static IGetTheConstructorToCreateAType greediest_ctor = x =>
      x.GetConstructors()
      .OrderByDescending(y => y.GetParameters().Count()).First();
  }
}
using System;
using System.Collections;
using System.Collections.Generic;
using app.containers.core;
using app.startup.stubs;

namespace app.containers.basic
{
  public class DependencyRegistration : IEnumerable<ICreateADependency>,
    IRegisterDependencyFactories
  {
    IList<ICreateADependency> factories = new List<ICreateADependency>();

    public void register<Contract, Implementation>()
    {
      factories.Add(new DependencyFactory(
        DelegateStubs.is_type<Contract>(), 
         new AutomaticDependencyFactory(typeof(Implementation),
          stubs.DelegateStubs.greediest_ctor,
          new LazyContainer())));
    }

    public void register<Contract>(Contract implementation)
    {
      factories.Add(new DependencyFactory(
        DelegateStubs.is_type<Contract>(), 
        new BasicFactory(() => implementation)));
    }

    class LazyContainer :IFetchDependencies
    {
      public Dependency an<Dependency>()
      {
        return Dependencies.fetch.an<Dependency>();
      }

      public object an(Type dependency)
      {
        return Dependencies.fetch.an(dependency);
      }
    }

    public IEnumerator<ICreateADependency> GetEnumerator()
    {
      return factories.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }
  }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using app.containers.core;
using app.test_utilities;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace app.containers.basic
{
  [Subject(typeof(AutomaticDependencyFactory))]
  public class AutomaticDependencyFactorySpecs
  {
    public abstract class concern : Observes<ICreateOneDependency,
      AutomaticDependencyFactory>
    {
    }

    public class when_creating_a_dependency_that_has_other_dependencies : concern
    {
      Establish c = () =>
      {
        other_item = new OtherItem();
        connection = fake.an<IDbConnection>();
        command = fake.an<IDbCommand>();

        container = depends.on<IFetchDependencies>();
        depends.on(typeof(ThingWithDependencies));

        var greediest_ctor = Objects.expressions.to_target<ThingWithDependencies>()
          .ctor_pointed_at_by(() => new ThingWithDependencies(null, null, null));

        depends.on<IGetTheConstructorToCreateAType>(x =>
        {
          x.ShouldEqual(typeof(ThingWithDependencies));
          return greediest_ctor;
        });

        container.setup(x => x.an(typeof(IDbConnection))).Return(connection);
        container.setup(x => x.an(typeof(IDbCommand))).Return(command);
        container.setup(x => x.an(typeof(OtherItem))).Return(other_item);
      };

      Because b = () =>
        result = sut.create();

      It returns_the_item_fully_formed_with_all_its_dependencies_provided = () =>
      {
        var item = result.ShouldBeAn<ThingWithDependencies>();
        item.command.ShouldEqual(command);
        item.connection.ShouldEqual(connection);
        item.other_item.ShouldEqual(other_item);
      };

      static object result;
      static OtherItem other_item;
      static IDbConnection connection;
      static IDbCommand command;
      static IFetchDependencies container;
    }

    public class ThingWithDependencies
    {
      public IDbCommand command { get; private set; }
      public IDbConnection connection { get; private set; }
      public OtherItem other_item { get; private set; }

      public ThingWithDependencies(IDbCommand command)
      {
        this.command = command;
      }

      public ThingWithDependencies(IDbCommand command, IDbConnection connection, OtherItem other_item)
      {
        this.command = command;
        this.connection = connection;
        this.other_item = other_item;
      }
    }

    public class OtherItem
    {
    }
  }

  public class AutomaticDependencyFactory : ICreateOneDependency
  {
    Type type_to_create;
    IGetTheConstructorToCreateAType ctor_picker;
    IFetchDependencies container;

    public AutomaticDependencyFactory(Type type_to_create, IGetTheConstructorToCreateAType ctor_picker, IFetchDependencies container)
    {
      this.type_to_create = type_to_create;
      this.ctor_picker = ctor_picker;
      this.container = container;
    }

    public object create()
    {
      var ctor = ctor_picker(type_to_create);

      var parameters = ctor.GetParameters()
        .Select(x => container.an(x.ParameterType)).ToArray();

      return ctor.Invoke(parameters);
    }
  }
}
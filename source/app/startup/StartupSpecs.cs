using System;
using System.Collections.Generic;
using app.containers.basic;
using app.containers.core;
using app.web.aspnet;
using app.web.aspnet.stubs;
using app.web.core;
using app.web.core.stubs;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;
using DelegateStubs = app.startup.stubs.DelegateStubs;

namespace app.startup
{
  [Subject(typeof(Startup))]
  public class StartupSpecs
  {
    public abstract class concern : Observes
    {
    }

    public class when_the_startup_command_finishes_running : concern
    {
      Because b = () =>
        Startup.run();

      It key_application_services_should_be_available_to_be_service_located = () =>
      {
        Dependencies.fetch.an<IHandleAllIncomingWebRequests>().ShouldBeAn<GeneralRequestHandler>();
      };
    }
  }

  public class Startup
  {
    static IList<ICreateADependency> factories;
    static IFetchDependencies container;

    public static void run()
    {
      configure_the_container();
      configure_front_controller_component_layer();
    }

    static void configure_front_controller_component_layer()
    {
      register_factory<IHandleAllIncomingWebRequests, GeneralRequestHandler>(() => new GeneralRequestHandler(
        container.an<IGetHandlersForRequests>()));

      register_factory<IGetHandlersForRequests, Handlers>(() => new Handlers(
        container.an<IEnumerable<IHandleOneRequest>>(),
        container.an<ICreateAHandlerWhenNoHandlersCanProcessTheRequest>()));

      register_factory<IEnumerable<IHandleOneRequest>, StubRequestHandlers>(() => new StubRequestHandlers());

      register_factory<IDisplayInformation, WebFormDisplayEngine>(() =>
        new WebFormDisplayEngine(container.an<IGetTheCurrentRequest>(),
          container.an<ICreateWebFormsToDisplayReports>()));

      register_factory<ICreateWebFormsToDisplayReports,
        WebFormViewFactory>(() => new WebFormViewFactory(
          container.an<IGetPathsToViews>(),
          container.an<ICreatePageInstances>()));

      register_factory<IGetPathsToViews>(new StubPathRegistry());
      register_factory(web.core.stubs.DelegateStubs.create_missing_handler);
      register_factory(web.aspnet.stubs.DelegateStubs.get_the_current_request);
      register_factory(web.aspnet.stubs.DelegateStubs.create_page);
      register_factory(web.aspnet.stubs.DelegateStubs.create_controller_request);
    }

    static void configure_the_container()
    {
      factories = new List<ICreateADependency>();

      IGetFactoriesForDependencies factory_registry = new DependencyFactories(factories,
        DelegateStubs.create_missing_dependency_factory);

      container = new BasicContainer(factory_registry,
        DelegateStubs.create_exception_for_failure_to_create_dependency);

      Dependencies.configure_the_container = () => container;
    }

    static void register_factory<Contract, Implementation>(Func<Implementation> factory)
    {
      factories.Add(new DependencyFactory(DelegateStubs.is_type<Contract>(),
        new BasicFactory(() => factory())));
    }

    static void register_factory<Contract>(Contract implementation)
    {
      factories.Add(new DependencyFactory(DelegateStubs.is_type<Contract>(),
        new BasicFactory(() => implementation)));
    }
  }
}
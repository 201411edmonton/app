﻿using app.containers.core;
using app.startup.steps;
using app.web.core;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

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
        Dependencies.fetch.an<IHandleAllIncomingWebRequests>().ShouldBeAn<GeneralRequestHandler>();
    }
  }

  public class Startup
  {
    public static void run()
    {
      Start.by.running<ConfigureTheContainer>()
        .then<ConfigureTheFrontController>()
        .finish_with<ConfiguringRoutes>();
    }
  }
}
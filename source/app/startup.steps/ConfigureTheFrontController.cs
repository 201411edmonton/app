using System.Collections.Generic;
using app.web.aspnet;
using app.web.aspnet.stubs;
using app.web.core;
using app.web.core.stubs;
using DelegateStubs = app.web.core.stubs.DelegateStubs;

namespace app.startup.steps
{
  public class ConfigureTheFrontController : IRunAStartupStep
  {
    IHelpConfigureStartupPipelines startup_configuration;

    public ConfigureTheFrontController(IHelpConfigureStartupPipelines startup_configuration)
    {
      this.startup_configuration = startup_configuration;
    }

    public void run()
    {
      startup_configuration.register_factory<IHandleAllIncomingWebRequests, GeneralRequestHandler>();
      startup_configuration.register_factory<IGetHandlersForRequests, Handlers>();
      startup_configuration.register_factory<IEnumerable<IHandleOneRequest>, StubRequestHandlers>();
      startup_configuration.register_factory<IDisplayInformation, WebFormDisplayEngine>();
      startup_configuration.register_factory<ICreateWebFormsToDisplayReports, WebFormViewFactory>();
      startup_configuration.register_factory<IGetPathsToViews, StubPathRegistry>();

      startup_configuration.register_factory(DelegateStubs.create_missing_handler);
      startup_configuration.register_factory(web.aspnet.stubs.DelegateStubs.get_the_current_request);
      startup_configuration.register_factory(web.aspnet.stubs.DelegateStubs.create_page);
      startup_configuration.register_factory(web.aspnet.stubs.DelegateStubs.create_controller_request);
    }
  }
}

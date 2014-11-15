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
    IHelpConfigureStartupPipelines startup;

    public ConfigureTheFrontController(IHelpConfigureStartupPipelines startup)
    {
      this.startup = startup;
    }

    public void run()
    {
      var component_registration = startup.component_registration;

      component_registration.register<IHandleAllIncomingWebRequests, GeneralRequestHandler>();
      component_registration.register<IGetHandlersForRequests, Handlers>();
      component_registration.register<IEnumerable<IHandleOneRequest>, StubRequestHandlers>();
      component_registration.register<IDisplayInformation, WebFormDisplayEngine>();
      component_registration.register<ICreateWebFormsToDisplayReports, WebFormViewFactory>();
      component_registration.register<IGetPathsToViews, StubPathRegistry>();

      component_registration.register(DelegateStubs.create_missing_handler);
      component_registration.register(web.aspnet.stubs.DelegateStubs.get_the_current_request);
      component_registration.register(web.aspnet.stubs.DelegateStubs.create_page);
      component_registration.register(web.aspnet.stubs.DelegateStubs.create_controller_request);
    }
  }
}
using System.Collections.Generic;
using System.Linq;
using app.web.aspnet.stubs;
using app.web.core.stubs;

namespace app.web.core
{
  public class Handlers : IGetHandlersForRequests
  {
    IEnumerable<IHandleOneRequest> all_handlers;
    ICreateAHandlerWhenNoHandlersCanProcessTheRequest special_case_factory;

    public Handlers(IEnumerable<IHandleOneRequest> all_handlers,
      ICreateAHandlerWhenNoHandlersCanProcessTheRequest special_case_factory)
    {
      this.all_handlers = all_handlers;
      this.special_case_factory = special_case_factory;
    }

    public Handlers():this(new StubRequestHandlers(),
      WebDelegateStubs.create_missing_handler)
    {
    }

    public IHandleOneRequest get_the_handler_that_can_run(IProvideRequestDetails request)
    {
      return all_handlers.FirstOrDefault(x => x.can_handle(request))
             ?? special_case_factory(request);
    }
  }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace app.web.core
{
  public class Handlers : IGetHandlersForRequests
  {
    IEnumerable<IHandleOneRequest> all_handlers;

    public Handlers(IEnumerable<IHandleOneRequest> all_handlers)
    {
      this.all_handlers = all_handlers;
    }

    public IHandleOneRequest get_the_handler_that_can_run(IProvideRequestDetails request)
    {
      return all_handlers.First(x => x.can_handle(request));
      throw new NotImplementedException();
    }
  }
}
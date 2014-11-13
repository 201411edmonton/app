using System.Collections.Generic;

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
      throw new System.NotImplementedException();
    }
  }
}
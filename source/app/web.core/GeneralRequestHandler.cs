namespace app.web.core
{
  public class GeneralRequestHandler : IHandleAllIncomingWebRequests
  {
    IGetHandlersForRequests handler_registry;

    public GeneralRequestHandler(IGetHandlersForRequests handler_registry)
    {
      this.handler_registry = handler_registry;
    }

    public GeneralRequestHandler():this(new Handlers())
    {
    }

    public void process(IProvideRequestDetails request)
    {
      var handler = handler_registry.get_the_handler_that_can_run(request);
      handler.process(request);
    }
  }
}
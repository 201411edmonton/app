namespace app.web.core
{
  public interface IGetHandlersForRequests
  {
    IHandleOneRequest get_the_handler_that_can_run(IProvideRequestDetails request);
  }
}
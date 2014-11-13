namespace app.web.core
{
  public delegate IHandleOneRequest ICreateAHandlerWhenNoHandlersCanProcessTheRequest(IProvideRequestDetails request);
}
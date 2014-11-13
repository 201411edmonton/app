namespace app.web.core
{
  public interface IHandleAllIncomingWebRequests
  {
    void process(IProvideRequestDetails request);
  }
}
namespace app.web.core
{
  public interface IHandleOneRequest
  {
    void process(IProvideRequestDetails request);
    bool can_handle(IProvideRequestDetails requestt);
  }
}
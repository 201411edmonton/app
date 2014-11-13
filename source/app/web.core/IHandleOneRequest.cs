namespace app.web.core
{
  public interface IHandleOneRequest : ISupportAFeature
  {
    bool can_handle(IProvideRequestDetails request);
  }
}
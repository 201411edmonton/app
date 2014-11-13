namespace app.web.core
{
  public class RequestHandler : IHandleOneRequest
  {
    IMatchARequest request_specification;
    IRunAFeature feature;

    public RequestHandler(IMatchARequest request_specification, IRunAFeature feature)
    {
      this.request_specification = request_specification;
      this.feature = feature;
    }

    public void process(IProvideRequestDetails request)
    {
      feature(request);
    }

    public bool can_handle(IProvideRequestDetails request)
    {
      return request_specification(request);
    }
  }
}
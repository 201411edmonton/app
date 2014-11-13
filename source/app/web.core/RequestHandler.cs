namespace app.web.core
{
  public class RequestHandler : IHandleOneRequest
  {
    IMatchARequest request_specification;
    ISupportAFeature feature;

    public RequestHandler(IMatchARequest request_specification, ISupportAFeature feature)
    {
      this.request_specification = request_specification;
      this.feature = feature;
    }

    public void process(IProvideRequestDetails request)
    {
      feature.process(request);
    }

    public bool can_handle(IProvideRequestDetails request)
    {
      return request_specification(request);
    }
  }
}
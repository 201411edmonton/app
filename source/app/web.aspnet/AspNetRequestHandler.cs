using System.Web;
using app.web.aspnet.stubs;
using app.web.core;

namespace app.web.aspnet
{
  public class AspNetRequestHandler : IHttpHandler
  {
    ICreateAControllerRequestFromAnAspNetRequest request_factory;
    IHandleAllIncomingWebRequests front_controller;

    public AspNetRequestHandler(IHandleAllIncomingWebRequests front_controller,
      ICreateAControllerRequestFromAnAspNetRequest request_factory)
    {
      this.front_controller = front_controller;
      this.request_factory = request_factory;
    }

    public AspNetRequestHandler():this(new GeneralRequestHandler(),
      DelegateStubs.create_controller_request)
    {
    }

    public void ProcessRequest(HttpContext context)
    {
      front_controller.process(request_factory(context));
    }

    public bool IsReusable
    {
      get { return true; }
    }
  }
}
using System;
using System.Web;
using System.Web.Compilation;
using app.web.core;

namespace app.web.aspnet.stubs
{
  public class DelegateStubs
  {
    public static ICreateAControllerRequestFromAnAspNetRequest create_controller_request = x =>
      new StubRequest();

    public static IGetTheCurrentRequest get_the_current_request = () => HttpContext.Current;

    public static ICreatePageInstances create_page = BuildManager.CreateInstanceFromVirtualPath;

    class StubRequest : IProvideRequestDetails
    {
      public InputModel map<InputModel>()
      {
        return Activator.CreateInstance<InputModel>();
      }
    }
  }
}
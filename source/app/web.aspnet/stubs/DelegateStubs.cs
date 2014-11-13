using System;
using app.web.core;

namespace app.web.aspnet.stubs
{
  public class DelegateStubs
  {
    public static ICreateAControllerRequestFromAnAspNetRequest create_controller_request = x =>
      new StubRequest();

    class StubRequest : IProvideRequestDetails
    {
      public InputModel map<InputModel>()
      {
        return Activator.CreateInstance<InputModel>();
      }
    }
  }
}
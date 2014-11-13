using System;
using app.web.core;

namespace app.web.aspnet.stubs
{
  public class WebDelegateStubs
  {
    public static ICreateAControllerRequestFromAnAspNetRequest create_controller_request = x =>
      new StubRequest();

    public static ICreateAHandlerWhenNoHandlersCanProcessTheRequest create_missing_handler = delegate
    {
      throw new NotImplementedException("There is no handler that can handle this request");
    };

    class StubRequest : IProvideRequestDetails
    {
      public InputModel map<InputModel>()
      {
        return Activator.CreateInstance<InputModel>();
      }
    }
  }
}
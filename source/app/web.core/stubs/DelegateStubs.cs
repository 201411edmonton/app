using System;

namespace app.web.core.stubs
{
  public class DelegateStubs
  {
    public static ICreateAHandlerWhenNoHandlersCanProcessTheRequest create_missing_handler = delegate
    {
      throw new NotImplementedException("There is no handler that can handle this request");
    };
  }
}
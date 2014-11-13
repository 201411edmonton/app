using System.Web;
using app.web.core;

namespace app.web.aspnet
{
  public delegate IProvideRequestDetails ICreateAControllerRequestFromAnAspNetRequest(HttpContext context);
}
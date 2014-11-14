using System.Web;
using System.Web.UI;

namespace app.web.aspnet
{
  public interface IDisplayA<Report> : IHttpHandler
  {
    Report report { get; set; }
  }

  public class DataBoundView<Report> : Page, IDisplayA<Report>
  {
    public Report report { get; set; }
  }
}
using System.Web;

namespace app.web.aspnet
{
  public interface ICreateWebFormsToDisplayReports
  {
    IHttpHandler create_view_to_display<Report>(Report report);
  }
}
namespace app.web.core
{
  public delegate Report IGetAReport<out Report>(IProvideRequestDetails request);

  public interface IGetAReportFromARequest<out Report>
  {
    Report fetch_using(IProvideRequestDetails request);
  }
}
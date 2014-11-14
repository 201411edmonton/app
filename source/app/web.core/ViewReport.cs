namespace app.web.core
{
  public class ViewReport<Report> : ISupportAFeature
  {
    IDisplayInformation display_engine;
    IGetAReport<Report> query;

    public ViewReport(IDisplayInformation display_engine, IGetAReport<Report> query)
    {
      this.display_engine = display_engine;
      this.query = query;
    }

    public void process(IProvideRequestDetails request)
    {
      var report = query(request);
      display_engine.display(report);
    }
  }
}
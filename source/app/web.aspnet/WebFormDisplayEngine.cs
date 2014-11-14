using System.Web;
using app.test_utilities;
using app.web.aspnet.stubs;
using app.web.core;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace app.web.aspnet
{
  [Subject(typeof(WebFormDisplayEngine))]
  public class WebFormDisplayEngineSpecs
  {
    public abstract class concern : Observes<IDisplayInformation,
      WebFormDisplayEngine>
    {
    }

    public class when_displaying_a_report : concern
    {
      Establish c = () =>
      {
        report = new AReport();
        view = fake.an<IHttpHandler>();
        web_form_view_factory = depends.on<ICreateWebFormsToDisplayReports>();

        web_form_view_factory.setup(x => x.create_view_to_display(report)).Return(view);
        the_current_request = Objects.web.create_http_context();
        depends.on<IGetTheCurrentRequest>(() => the_current_request);
      };

      Because b = () =>
        sut.display(report);

      It tells_the_view_to_show_itself = () =>
        view.received(x => x.ProcessRequest(the_current_request));

      static AReport report;
      static ICreateWebFormsToDisplayReports web_form_view_factory;
      static IHttpHandler view;
      static HttpContext the_current_request;
    }

    public class AReport
    {
    }
  }

  public class WebFormDisplayEngine : IDisplayInformation
  {
    ICreateWebFormsToDisplayReports view_factory;
    IGetTheCurrentRequest current_request;

    public WebFormDisplayEngine(IGetTheCurrentRequest current_request, ICreateWebFormsToDisplayReports view_factory)
    {
      this.current_request = current_request;
      this.view_factory = view_factory;
    }

    public void display<ReportModel>(ReportModel report)
    {
      var view = view_factory.create_view_to_display(report);
      view.ProcessRequest(current_request());
    }
  }
}
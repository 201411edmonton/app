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
        web_form_view_factory = depends.on<ICreateWebFormsToDisplayReports>();
      };

      Because b = () => 
        sut.display(report);

      It creates_the_view_that_can_display_the_report = () =>
        web_form_view_factory.received(x => x.create_view_to_display(report));

      static AReport report;
      static ICreateWebFormsToDisplayReports web_form_view_factory;
    }
  }

  public interface ICreateWebFormsToDisplayReports
  {
    void create_view_to_display<Report>(Report report);
  }

  public class AReport
  {
  }
}

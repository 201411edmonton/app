using app.web.core;
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
      };

      Because b = () => 
        sut.display(report);

      It displays_using_the_report_model = () =>
      {
      };

      static AReport report;
    }
  }

  public class AReport
  {
  }
}

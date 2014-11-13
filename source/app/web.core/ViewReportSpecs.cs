using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace app.web.core
{
  [Subject(typeof(ViewReport<>))]
  public class ViewReportSpecs
  {
    public abstract class concern : Observes<ISupportAFeature,
      ViewReport<MyReport>>
    {
    }

    public class when_run : concern
    {
      Establish c = () =>
      {
        request = fake.an<IProvideRequestDetails>();
        report = new MyReport();
        display_engine = depends.on<IDisplayInformation>();

        depends.on<IGetAReport<MyReport>>(x =>
        {
          x.ShouldEqual(request);
          return report;
        });
      };

      Because b = () =>
        sut.process(request);

      It displays_the_report_fetched_using_its_query = () =>
        display_engine.received(x => x.display(report));

      static IDisplayInformation display_engine;
      static IProvideRequestDetails request;
      static MyReport report;
    }

    public class MyReport
    {
    }
  }
}
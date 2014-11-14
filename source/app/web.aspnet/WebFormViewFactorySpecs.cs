using System;
using System.Web;
using System.Web.UI;
using app.web.aspnet.stubs;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace app.web.aspnet
{
  [Subject(typeof(WebFormViewFactory))]
  public class WebFormViewFactorySpecs
  {
    public abstract class concern : Observes<ICreateWebFormsToDisplayReports,
      WebFormViewFactory>
    {
    }

    public class when_creating_a_view : concern
    {
      Establish c = () =>
      {
        path_registry = depends.on<IGetPathsToViews>();
        path_to_view = "blah.aspx";
        path_registry.setup(x => x.get_path_to_view_for<TheReport>()).Return(path_to_view);

        report = new TheReport();
        view = fake.an<IDisplayA<TheReport>>();

        depends.on<ICreatePageInstances>((path, type) =>
        {
          path.ShouldEqual(path_to_view);
          type.ShouldEqual(typeof(IDisplayA<TheReport>));
          return view;
        });
      };

      Because b = () =>
        result = sut.create_view_to_display(report);

      It provides_the_view_with_its_report = () =>
        view.report.ShouldEqual(report);
        
      It returns_the_view_instance = () =>
        result.ShouldEqual(view);

      static TheReport report;
      static IGetPathsToViews path_registry;
      static IHttpHandler result;
      static IDisplayA<TheReport> view;
      static string path_to_view;
    }

    public class TheReport
    {
    }
  }

  public interface IDisplayA<Report> : IHttpHandler
  {
    Report report { get; set; }
  }

  public class DataBoundView<Report> : Page, IDisplayA<Report>
  {
    public Report report { get; set; }
  }

  public delegate object ICreatePageInstances(string path, Type type);

  public interface IGetPathsToViews
  {
    string get_path_to_view_for<Report>();
  }

  public class WebFormViewFactory : ICreateWebFormsToDisplayReports
  {
    IGetPathsToViews path_registry;
    ICreatePageInstances page_factory;

    public WebFormViewFactory(IGetPathsToViews path_registry, ICreatePageInstances page_factory)
    {
      this.path_registry = path_registry;
      this.page_factory = page_factory;
    }

    public IHttpHandler create_view_to_display<Report>(Report report)
    {
      var path = path_registry.get_path_to_view_for<Report>();
      var handler = page_factory(path, typeof(IDisplayA<Report>));
      var report_handler = (IDisplayA<Report>) handler;
      report_handler.report = report;

      return report_handler;
    }
  }
}
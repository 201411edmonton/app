using app.catalog_browsing.stubs;
using app.web.core;
using app.web.core.stubs;

namespace app.catalog_browsing
{
  public class ViewTheMainDepartments : ISupportAFeature
  {
    IFindDepartments departments;
    IDisplayInformation display_engine;

    public ViewTheMainDepartments(IFindDepartments departments, IDisplayInformation display_engine)
    {
      this.departments = departments;
      this.display_engine = display_engine;
    }

    public ViewTheMainDepartments():this(new StubStoreCatalog(),
      new StubDisplayEngine())
    {
    }

    public void process(IProvideRequestDetails request)
    {
      var results = departments.get_the_main_departments();
      display_engine.display(results);
    }
  }
}
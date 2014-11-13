using app.catalog_browsing.stubs;
using app.web.core;
using app.web.core.stubs;

namespace app.catalog_browsing
{
  public class ViewTheDepartmentsInADepartment : ISupportAFeature
  {
    IDisplayInformation display_engine;
    IFindDepartments departments;

    public ViewTheDepartmentsInADepartment(IFindDepartments departments, IDisplayInformation display_engine)
    {
      this.departments = departments;
      this.display_engine = display_engine;
    }

    public ViewTheDepartmentsInADepartment():this(new StubStoreCatalog(), 
      new StubDisplayEngine())
    {
    }

    public void process(IProvideRequestDetails request)
    {
      var results = departments.get_departments_using(request.map<GetSubDepartmentsInput>());
      display_engine.display(results);
    }
  }
}
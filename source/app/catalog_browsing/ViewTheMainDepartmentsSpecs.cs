using System.Collections.Generic;
using app.web.core;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace app.catalog_browsing
{
  [Subject(typeof(ViewTheMainDepartments))]
  public class ViewTheMainDepartmentsSpecs
  {
    public abstract class concern : Observes<ISupportAFeature,
      ViewTheMainDepartments>
    {
    }

    public class when_run : concern
    {
      Establish c = () =>
      {
        request = fake.an<IProvideRequestDetails>();
        the_main_departments = new List<DepartmentLineItem>();

        departments = depends.on<IFindDepartments>();
        display_engine = depends.on<IDisplayInformation>();

        departments.setup(x => x.get_the_main_departments()).Return(the_main_departments);
      };

      Because b = () =>
        sut.process(request);

      It displays_the_main_departments = () =>
        display_engine.received(x => x.display(the_main_departments));

      static IFindDepartments departments;
      static IProvideRequestDetails request;
      static IDisplayInformation display_engine;
      static IEnumerable<DepartmentLineItem> the_main_departments;
    }
  }
}
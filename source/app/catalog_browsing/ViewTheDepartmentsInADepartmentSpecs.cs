using System.Collections.Generic;
using app.web.core;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace app.catalog_browsing
{
  [Subject(typeof(ViewTheDepartmentsInADepartment))]
  public class ViewTheDepartmentsInADepartmentSpecs
  {
    public abstract class concern : Observes<ISupportAFeature,
      ViewTheDepartmentsInADepartment>
    {
    }

    public class when_run : concern
    {
      Establish c = () =>
      {
        request = fake.an<IProvideRequestDetails>();
        sub_departments = new List<DepartmentLineItem>();
        display_engine = depends.on<IDisplayInformation>();
        input = new GetSubDepartmentsInput();
        departments = depends.on<IFindDepartments>();

        request.setup(x => x.map<GetSubDepartmentsInput>()).Return(input);

        departments.setup(x => x.get_departments_using(input)).Return(sub_departments);
      };

      Because b = () =>
        sut.process(request);

      It displays_the_departments_in_a_department = () =>
        display_engine.received(x => x.display(sub_departments));

      static IDisplayInformation display_engine;
      static IEnumerable<DepartmentLineItem> sub_departments;
      static IProvideRequestDetails request;
      static IFindDepartments departments;
      static GetSubDepartmentsInput input;
    }
  }
}
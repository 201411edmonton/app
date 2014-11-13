using System.Collections.Generic;
using app.web.core;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace app.catalog_browsing
{
  [Subject(typeof(ViewTheProductsInADepartment))]
  public class ViewTheProductsInADepartmentSpecs
  {
    public abstract class concern : Observes<ISupportAFeature,
      ViewTheProductsInADepartment>
    {
    }

    public class when_run : concern
    {
      Establish c = () =>
      {
        request = fake.an<IProvideRequestDetails>();
        products = depends.on<IFindProducts>();
        display_engine = depends.on<IDisplayInformation>();

        department_products = new List<ProductLineItem>();
        input = new GetProductsInDepartmentInput();

        request.setup(x => x.map<GetProductsInDepartmentInput>()).Return(input);
        products.setup(x => x.get_products_using(input)).Return(department_products);
      };

      Because b = () =>
        sut.process(request);

      It displays_the_products_in_a_department = () =>
        display_engine.received(x => x.display(department_products));

      static IDisplayInformation display_engine;
      static IEnumerable<ProductLineItem> department_products;
      static IProvideRequestDetails request;
      static IFindProducts products;
      static GetProductsInDepartmentInput input;
    }
  }
}

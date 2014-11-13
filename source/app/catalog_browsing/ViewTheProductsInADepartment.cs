using app.catalog_browsing.stubs;
using app.web.core;
using app.web.core.stubs;

namespace app.catalog_browsing
{
  public class ViewTheProductsInADepartment : ISupportAFeature
  {
    IDisplayInformation display_engine;
    IFindProducts products;

    public ViewTheProductsInADepartment(IDisplayInformation display_engine, IFindProducts products)
    {
      this.display_engine = display_engine;
      this.products = products;
    }

    public ViewTheProductsInADepartment() : this(new StubDisplayEngine(),
      new StubStoreCatalog())
    {
    }

    public void process(IProvideRequestDetails request)
    {
      display_engine.display(products.get_products_using(request.map<GetProductsInDepartmentInput>()));
    }
  }
}

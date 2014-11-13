using System.Collections.Generic;
using System.Linq;

namespace app.catalog_browsing.stubs
{
  public class StubStoreCatalog : IFindDepartments, IFindProducts
  {
    public IEnumerable<DepartmentLineItem> get_the_main_departments()
    {
      return Enumerable.Range(1, 100).Select(x => new DepartmentLineItem{ name = x.ToString("Main Department 0")});
    }

    public IEnumerable<DepartmentLineItem> get_departments_using(GetSubDepartmentsInput input)
    {
      return Enumerable.Range(1, 100).Select(x => new DepartmentLineItem{ name = x.ToString("Sub Department 0")});
    }

    public IEnumerable<ProductLineItem> get_products_using(GetProductsInDepartmentInput input)
    {
      return Enumerable.Range(1, 100).Select(x => new ProductLineItem{ name = x.ToString("Product 0")});
    }
  }
}
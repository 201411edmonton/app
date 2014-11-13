using System.Collections.Generic;

namespace app.catalog_browsing
{
  public interface IFindProducts
  {
    IEnumerable<ProductLineItem> get_products_using(GetProductsInDepartmentInput input);
  }
}

using System.Collections.Generic;
using System.Linq;
using app.web.core;

namespace app.catalog_browsing.stubs
{
  public class GetTheProductsInADepartment : IGetAReportFromARequest<IEnumerable<ProductLineItem>>
  {
    public IEnumerable<ProductLineItem> fetch_using(IProvideRequestDetails request)
    {
      return Enumerable.Range(1, 100).Select(x => new ProductLineItem{ name = x.ToString("Product 0")});
    }
  }
}
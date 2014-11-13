using System.Collections.Generic;
using System.Linq;
using app.web.core;

namespace app.catalog_browsing.stubs
{
  public class GetTheMainDepartments : IGetAReportFromARequest<IEnumerable<DepartmentLineItem>>
  {
    public IEnumerable<DepartmentLineItem> fetch_using(IProvideRequestDetails request)
    {
      return Enumerable.Range(1, 100).Select(x => new DepartmentLineItem {name = x.ToString("Main Department 0")});
    }
  }
}
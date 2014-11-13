using System.Collections;
using System.Collections.Generic;
using app.catalog_browsing;

namespace app.web.core.stubs
{
  public class StubRequestHandlers : IEnumerable<IHandleOneRequest>
  {
    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public IEnumerator<IHandleOneRequest> GetEnumerator()
    {
      yield return new RequestHandler(x => true, new ViewTheProductsInADepartment());
      yield return new RequestHandler(x => true, new ViewTheMainDepartments());
      yield return new RequestHandler(x => true, new ViewTheDepartmentsInADepartment());
    }
  }
}
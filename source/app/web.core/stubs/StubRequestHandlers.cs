using System;
using System.Collections;
using System.Collections.Generic;
using app.catalog_browsing.stubs;
using app.containers.core;

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
      yield return handler_to_fetch_report(new GetTheMainDepartments().fetch_using);
      yield return handler_to_fetch_report(new GetTheDepartmentsInADepartment().fetch_using);
      yield return handler_to_fetch_report(new GetTheProductsInADepartment().fetch_using);
    }

    public IHandleOneRequest handler_to_fetch_report<Query, Report>() where Query : IGetAReportFromARequest<Report>,
      new()
    {
      return handler_to_fetch_report(new Query().fetch_using);
    }

    public IHandleOneRequest handler_to_fetch_report<Report>(IGetAReport<Report> query)
    {
      return new RequestHandler(x => true, create_report_viewer(query));
    }

    static IRunAFeature create_report_viewer<Report>(IGetAReport<Report> query)
    {
      return new ViewReport<Report>(Dependencies.fetch.an<IDisplayInformation>(),
        query).process;
    }
  }
}
using System;
using System.Collections.Generic;
using app.catalog_browsing;

namespace app.web.aspnet.stubs
{
  public class StubPathRegistry : IGetPathsToViews
  {
    public string get_path_to_view_for<Report>()
    {
      var views = new Dictionary<Type, string>
      {
        {typeof(IEnumerable<DepartmentLineItem>), "~/views/DeparmentBrowser.aspx"},
        {typeof(IEnumerable<ProductLineItem>), "~/views/ProductBrowser.aspx"}
      };


      if (views.ContainsKey(typeof(Report))) return views[typeof(Report)];

      throw new NotImplementedException(string.Format("There is no view that can display", typeof(Report)));
    }
  }
}
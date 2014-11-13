using System.Collections.Generic;

namespace app.catalog_browsing
{
  public interface IFindDepartments
  {
    IEnumerable<DepartmentLineItem> get_the_main_departments();
    IEnumerable<DepartmentLineItem> get_departments_using(GetSubDepartmentsInput input);
  }
}
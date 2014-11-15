using System;
using System.Linq.Expressions;
using Machine.Specifications;

namespace app.spikes
{
  public class ExpressionSpikes
  {
    It can_create_an_expression_by_hand = () =>
    {
      Func<int, bool> is_even = x => x%2 == 0;
      is_even(2).ShouldBeTrue();

      var dynamic_even = create_is_even_using_expression_trees();
      var compiled = dynamic_even.Compile();

      compiled(2).ShouldBeTrue();
    };

    static Expression<Func<int, bool>> create_is_even_using_expression_trees()
    {
      var parameter = Expression.Parameter(typeof(int), "x");
      var zero = Expression.Constant(0);
      var two = Expression.Constant(2);

      var mod_2 = Expression.Modulo(parameter, two);
      var equal_0 = Expression.Equal(mod_2, zero);

      var dynamic_even = Expression.Lambda<Func<int, bool>>(equal_0,
        parameter);

      return dynamic_even;
    }
  }
}
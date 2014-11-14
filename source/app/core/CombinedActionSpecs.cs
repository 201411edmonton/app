using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace app.core
{
  [Subject(typeof(CombinedAction))]
  public class CombinedActionSpecs
  {
    public abstract class concern : Observes<IRunAnAction,
      CombinedAction>
    {
    }

    public class when_run : concern
    {
      Establish c = () =>
      {
        IRun first = () => first_ran = true;
        IRun second = () => second_ran = true;

        sut_factory.create_using(() => new CombinedAction(first, second));
      };

      Because b = () =>
        sut.run();

      It runs_both_of_its_actions = () =>
      {
        first_ran.ShouldBeTrue();
        second_ran.ShouldBeTrue();
      };

      static bool first_ran;
      static bool second_ran;
    }
  }

  public class CombinedAction : IRunAnAction
  {
    IRun first;
    IRun second;

    public CombinedAction(IRun first, IRun second)
    {
      this.first = first;
      this.second = second;
    }

    public void run()
    {
      first();
      second();
    }
  }
}
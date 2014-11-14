namespace app.core.stubs
{
  public class DelegateStubs
  {
    public static ICombineActions combine_actions = (first, second) =>
      new CombinedAction(first.run, second.run);
  }
}
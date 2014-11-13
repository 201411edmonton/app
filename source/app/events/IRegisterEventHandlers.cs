namespace app.events
{
  public interface IRegisterEventHandlers
  {
    void register_handler<Event>(IHandle<Event> handler);
  }
}
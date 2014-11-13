namespace app.events
{
  public interface IHandle<in Event>
  {
    void handle(Event event_data);
  }
}
namespace app.events
{
  public interface IPublishEvents
  {
    void publish<Event>(Event data);
  }
}
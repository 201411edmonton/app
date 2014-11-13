using System;
using System.Collections.Generic;
using app.core;

namespace app.events
{
  public class EventBroker : IPublishEvents, IRegisterEventHandlers
  {
    public IDictionary<Type, IList<IHandleEvents>> event_handlers = new Dictionary<Type, IList<IHandleEvents>>();

    public void register_handler<Event>(IHandle<Event> handler)
    {
      get_the_handlers_for<Event>().Add(new GenericHandler<Event>(handler));
    }

    public class GenericHandler<Event> : IHandle<Event>, IHandleEvents
    {
      public IHandle<Event> real_handler;

      public GenericHandler(IHandle<Event> real_handler)
      {
        this.real_handler = real_handler;
      }

      public void handle(Event event_data)
      {
        real_handler.handle(event_data);
      }

      public void handle_event(object event_data)
      {
        handle((Event) event_data);
      }
    }

    public virtual void publish<Event>(Event data)
    {
      get_the_handlers_for<Event>().each(x => x.handle_event(data));
    }

    IList<IHandleEvents> get_the_handlers_for<T>()
    {
      if (event_handlers.ContainsKey(typeof(T)))
        return event_handlers[typeof(T)];

      var handlers = new  List<IHandleEvents>();
      event_handlers[typeof(T)] = handlers;
      return handlers;
    }
  }
}
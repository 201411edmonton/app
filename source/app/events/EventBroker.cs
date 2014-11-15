using System;
using System.Collections.Generic;
using System.Linq;
using app.core;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;
using Rhino.Mocks;

namespace app.events
{
  public interface IRegisterEventHandlers
  {
    void register_handler<Event>(IHandle<Event> handler);
  }

  public interface IPublishEvents
  {
    void publish<Event>(Event data);
  }

  public class EventBroker : IPublishEvents, IRegisterEventHandlers
  {
    public IDictionary<Type, IList<IHandleEvents>> event_handlers = new Dictionary<Type, IList<IHandleEvents>>();

    public void register_handler<Event>(IHandle<Event> handler)
    {
      get_the_handlers_for<Event>().Add(new GenericHandler<Event>(handler));
    }

    class GenericHandler<Event> : IHandle<Event>, IHandleEvents
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
      VisitorExtensions.each(get_the_handlers_for<Event>(), x => x.handle_event(data));
    }

    IList<IHandleEvents> get_the_handlers_for<T>()
    {
      if (event_handlers.ContainsKey(typeof(T)))
        return event_handlers[typeof(T)];

      var handlers = new List<IHandleEvents>();
      event_handlers[typeof(T)] = handlers;
      return handlers;
    }
  }

  [Subject(typeof(EventBroker))]
  public class EventBrokerSpecs
  {
    public abstract class concern_for_handler_registration : Observes<IRegisterEventHandlers,
      EventBroker>
    {
    }

    public abstract class concern_for_publishing_events : Observes<IPublishEvents,
      EventBroker>
    {
    }

    public class MyHandler : IHandle<MyCustomEvent>,
      IHandleEvents
    {
      public void handle(MyCustomEvent event_data)
      {
      }

      public void handle_event(object event_data)
      {
      }
    }

    public class when_a_handler_is_registered : concern_for_handler_registration
    {
      Establish c = () =>
      {
        handlers = new Dictionary<Type, IList<IHandleEvents>>();
        depends.on(handlers);
        my_handler = new MyHandler();
      };

      Because b = () =>
        sut.register_handler(my_handler);

      It is_stored_in_the_list_of_handlers_by_event_type = () =>
      {
        handlers.Count.ShouldEqual(1);
      };

      static IDictionary<Type, IList<IHandleEvents>> handlers;
      static MyHandler my_handler;
    }

    public class when_an_event_is_published : concern_for_publishing_events
    {
      Establish c = () =>
      {
        the_event = new MyCustomEvent();
        illegitimate_handler = fake.an<IHandle<SomeOtherEvent>>();

        concrete_handlers = Enumerable.Range(1, 100).Select(x => fake.an<IHandle<MyCustomEvent>>()).ToList();

        sut_setup.run(x =>
        {
          IterationExtensions.each(concrete_handlers, y => x.register_handler(y));
          x.register_handler(illegitimate_handler);
        });
      };

      Because b = () =>
        sut.publish(the_event);

      It invokes_the_handler_method_for_all_the_listeners_of_the_event = () =>
      {
        IterationExtensions.each(concrete_handlers, x => FakeExtensions.received(x, y => y.handle(the_event)));
        FakeExtensions.never_received(illegitimate_handler, x => x.handle(Arg<SomeOtherEvent>.Is.Anything));
      };

      static MyCustomEvent the_event;
      static IList<IHandle<MyCustomEvent>> concrete_handlers;
      static IHandle<SomeOtherEvent> illegitimate_handler;
    }

    public class MyCustomEvent
    {
    }

    public class SomeOtherEvent
    {
    }
  }
}
using System;
using System.Collections.Generic;
using System.Linq;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;
using Rhino.Mocks;

namespace app.events
{
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
        var handler = handlers[typeof(MyCustomEvent)][0].ShouldBeAn<EventBroker.GenericHandler<MyCustomEvent>>();
        handler.real_handler.ShouldEqual(my_handler);
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
          concrete_handlers.each(y => x.register_handler(y));
          x.register_handler(illegitimate_handler);
        });
      };

      Because b = () =>
        sut.publish(the_event);

      It invokes_the_handler_method_for_all_the_listeners_of_the_event = () =>
      {
        concrete_handlers.each(x => x.received(y => y.handle(the_event)));
        illegitimate_handler.never_received(x => x.handle(Arg<SomeOtherEvent>.Is.Anything));
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
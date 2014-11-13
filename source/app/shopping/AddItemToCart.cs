using System;
using app.events;
using app.web.core;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace app.shopping
{
  public class ManageCartDetails : IHandle<ItemAddedToCart>
  {
    IWriteRecords writer;

    public ManageCartDetails(IWriteRecords writer)
    {
      this.writer = writer;
    }

    public void handle(ItemAddedToCart event_data)
    {
      writer.write();
    }
  }

  public abstract class concern : Observes
  {
  }

  public class when_adding_an_item_to_the_cart : concern
  {
    Establish c = () =>
    {
      request = fake.an<IProvideRequestDetails>();
      writer = fake.an<IWriteRecords>();
      broker = new EventBroker();
      manage_cart_details = new ManageCartDetails(writer);

      broker.register_handler(manage_cart_details);

      sut = new PublishEventAfterRunning<ItemAddedToCart>(broker,
        new AddItemToCart(),
        () => new ItemAddedToCart());
    };

    Because b = () =>
      sut.process(request);

    It causes_the_item_record_to_be_written_to_the_db = () =>
      writer.received(x => x.write());

    static IWriteRecords writer;
    static IProvideRequestDetails request;
    static EventBroker broker;
    static ManageCartDetails manage_cart_details;
    static PublishEventAfterRunning<ItemAddedToCart> sut;
  }

  public interface IWriteRecords
  {
    void write();
  }

  public delegate Event ICreateAnEvent<out Event>();

  public class PublishEventAfterRunning<Event> : ISupportAFeature
  {
    ISupportAFeature feature;
    EventBroker events;
    ICreateAnEvent<Event> event_builder;

    public PublishEventAfterRunning(EventBroker events, ISupportAFeature feature, ICreateAnEvent<Event> event_builder)
    {
      this.events = events;
      this.feature = feature;
      this.event_builder = event_builder;
    }

    public void process(IProvideRequestDetails request)
    {
      feature.process(request);
      events.publish(event_builder());
    }
  }

  public class AddItemToCart : ISupportAFeature
  {
    public void process(IProvideRequestDetails request)
    {
      Console.Out.WriteLine("Do some work");
    }
  }
}
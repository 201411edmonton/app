using System.Collections.Generic;
using System.Linq;
using app.web.core.stubs;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace app.web.core
{
  [Subject(typeof(Handlers))]
  public class HandlersSpecs
  {
    public abstract class concern : Observes<IGetHandlersForRequests,
      Handlers>
    {
    }

    public class when_getting_a_handler_for_a_request : concern
    {
      public class and_it_has_the_handler
      {
        Establish c = () =>
        {
          request = fake.an<IProvideRequestDetails>();
          all_handlers = Enumerable.Range(1, 1000).Select(x => fake.an<IHandleOneRequest>()).ToList();
          the_handler_that_can_process_the_request = fake.an<IHandleOneRequest>();

          all_handlers.Add(the_handler_that_can_process_the_request);
          the_handler_that_can_process_the_request.setup(x => x.can_handle(request)).Return(true);

          depends.on<IEnumerable<IHandleOneRequest>>(all_handlers);
        };

        Because b = () =>
          result = sut.get_the_handler_that_can_run(request);

        It returns_the_handler = () =>
          result.ShouldEqual(the_handler_that_can_process_the_request);

        static IHandleOneRequest result;
        static IHandleOneRequest the_handler_that_can_process_the_request;
        static IProvideRequestDetails request;
        static List<IHandleOneRequest> all_handlers;
      }

      public class and_it_does_not_have_the_handler
      {
        Establish c = () =>
        {
          request = fake.an<IProvideRequestDetails>();
          all_handlers = Enumerable.Range(1, 1000).Select(x => fake.an<IHandleOneRequest>()).ToList();
          depends.on<IEnumerable<IHandleOneRequest>>(all_handlers);
          special_case = fake.an<IHandleOneRequest>();

          depends.on<ICreateAHandlerWhenNoHandlersCanProcessTheRequest>(x =>
          {
            x.ShouldEqual(request);
            return special_case;
          });
        };

        Because b = () =>
          result = sut.get_the_handler_that_can_run(request);

        It returns_the_handler_created_by_the_special_case_factory = () =>
          result.ShouldEqual(special_case);

        static IHandleOneRequest result;
        static IProvideRequestDetails request;
        static List<IHandleOneRequest> all_handlers;
        static IHandleOneRequest special_case;
      }
    }
  }

  public class Handlers : IGetHandlersForRequests
  {
    IEnumerable<IHandleOneRequest> all_handlers;
    ICreateAHandlerWhenNoHandlersCanProcessTheRequest special_case_factory;

    public Handlers(IEnumerable<IHandleOneRequest> all_handlers,
      ICreateAHandlerWhenNoHandlersCanProcessTheRequest special_case_factory)
    {
      this.all_handlers = all_handlers;
      this.special_case_factory = special_case_factory;
    }

    public IHandleOneRequest get_the_handler_that_can_run(IProvideRequestDetails request)
    {
      return all_handlers.FirstOrDefault(x => x.can_handle(request))
             ?? special_case_factory(request);
    }
  }

  public interface IGetHandlersForRequests
  {
    IHandleOneRequest get_the_handler_that_can_run(IProvideRequestDetails request);
  }
}
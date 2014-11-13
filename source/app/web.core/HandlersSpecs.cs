using System.Collections.Generic;
using System.Linq;
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
    }
  }
}
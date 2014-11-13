using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace app.web.core
{
  [Subject(typeof(RequestHandler))]
  public class RequestHandlerSpecs
  {
    public abstract class concern : Observes<IHandleOneRequest,
      RequestHandler>
    {
    }

    public class when_determining_if_it_can_process_a_request : concern
    {
      Establish c = () =>
      {
        request = fake.an<IProvideRequestDetails>();

        depends.on<IMatchARequest>(x =>
        {
          x.ShouldEqual(request);
          return true;
        });
      };

      Because b = () =>
        result = sut.can_handle(request);

      It makes_it_decision_by_using_its_request_specification = () =>
        result.ShouldBeTrue();

      static bool result;
      static IProvideRequestDetails request;
    }

    public class when_handling_a_request : concern
    {
      Establish c = () =>
      {
        request = fake.an<IProvideRequestDetails>();
        depends.on<IRunAFeature>(x =>
        {
          ran = true;
        });
      };

      Because b = () =>
        sut.process(request);

      It runs_the_application_feature_for_the_request = () =>
        ran.ShouldBeTrue();

      static IProvideRequestDetails request;
      static bool ran;
    }
  }
}
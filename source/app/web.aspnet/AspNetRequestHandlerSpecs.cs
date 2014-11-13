using System.Web;
using app.test_utilities;
using app.web.core;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace app.web.aspnet
{
  [Subject(typeof(AspNetRequestHandler))]
  public class AspNetRequestHandlerSpecs
  {
    public abstract class concern : Observes<IHttpHandler,
      AspNetRequestHandler>
    {
    }

    public class when_processing_a_request : concern
    {
      Establish c = () =>
      {
        front_controller_based_request = fake.an<IProvideRequestDetails>();
        front_controller = depends.on<IHandleAllIncomingWebRequests>();
        a_new_asp_net_based_request = Objects.web.create_http_context();

        depends.on<ICreateAControllerRequestFromAnAspNetRequest>(x =>
        {
          x.ShouldEqual(a_new_asp_net_based_request);
          return front_controller_based_request;
        });
      };

      Because b = () =>
        sut.ProcessRequest(a_new_asp_net_based_request);

      It delegates_the_processing_of_a_new_controller_request_to_the_front_controller = () =>
        front_controller.received(x => x.process(front_controller_based_request));

      static IHandleAllIncomingWebRequests front_controller;
      static IProvideRequestDetails front_controller_based_request;
      static HttpContext a_new_asp_net_based_request;
    }
  }
}
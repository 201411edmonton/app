 using app.web.core;
 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;

namespace app.web.aspnet
{  
  [Subject(typeof(WebFormDisplayEngine))]  
  public class WebFormDisplayEngineSpecs
  {
    public abstract class concern : Observes<IDisplayInformation,
      WebFormDisplayEngine>
    {
        
    }

   
    public class when_displaying_a_report : concern
    {
      
        
       // Because sut.
        
       // It displays_using_the_report_model = () =>        
        
    }
  }
}

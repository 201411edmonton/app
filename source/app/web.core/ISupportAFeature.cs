namespace app.web.core
{
  public delegate void IRunAFeature(IProvideRequestDetails details);

  public interface ISupportAFeature
  {
    void process(IProvideRequestDetails request);
  }
}
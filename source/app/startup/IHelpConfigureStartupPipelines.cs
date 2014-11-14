namespace app.startup
{
  public interface IHelpConfigureStartupPipelines
  {
    void register_factory<Contract, Implementation>();
    void register_factory<Contract>(Contract implementation);
  }
}
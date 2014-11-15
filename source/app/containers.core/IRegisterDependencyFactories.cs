namespace app.containers.core
{
  public interface IRegisterDependencyFactories
  {
    void register<Contract, Implementation>();

    void register<Contract>(Contract implementation);
  }
}
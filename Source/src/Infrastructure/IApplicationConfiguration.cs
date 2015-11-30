namespace HomeManager.Infrastructure
{
    public interface IApplicationConfiguration
    {
        void ApplicationStart();

        void ApplicationEnd();
    }
}

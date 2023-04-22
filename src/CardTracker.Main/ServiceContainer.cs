namespace CardTracker.Main;

public static class ServiceContainer
{
    public static void AddLogging(IServiceCollection services)
    {
        services.AddLogging();
    }

    public static void AddDatabase(IServiceCollection services)
    {

    }
}

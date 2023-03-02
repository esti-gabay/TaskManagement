using lesson1.Controllers;
using lesson1.Interfaces;


namespace  lesson1.Utilities
{
public static class Helper
{
    public static void AddTasks(this IServiceCollection services)
    {
        services.AddSingleton<ITaskService, TaskService>();
    
    }
}
}
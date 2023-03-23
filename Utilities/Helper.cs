using lesson1.Services;
using lesson1.Interfaces;

namespace lesson1.Utilities
{
    public static class Helper
    {
        public static void AddTasks(this IServiceCollection services)
        {
            services.AddSingleton<ITaskService, TaskService>();

        }
        public static void AddUsers(this IServiceCollection services)
        {
            services.AddSingleton<IUserService, UserService>();

        }

        public static void AddLogService(this IServiceCollection services)
        {
            services.AddTransient<ILogService, LogService>();

        }

    }
}
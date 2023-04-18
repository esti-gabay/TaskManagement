
using lesson1.Interfaces;
using lesson1.Services;
using System.Text.Json;

namespace lesson1.Services
{
    public class TaskService : ITaskService
    {
        private IWebHostEnvironment? webHost;
        private string filePath;
        private List<Task> tasks { get; } = new List<Task>();

        public TaskService(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
            this.filePath = Path.Combine(webHost.ContentRootPath, "Data", "tasks.json");
            using (var jsonFile = File.OpenText(filePath))
            {
                tasks = JsonSerializer.Deserialize<List<Task>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }

        private void saveToFile()
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(tasks));
        }

        public List<Task> GetAll()
        {
            return tasks;
        }

        public List<Task> Get(string token)
        {
            string UserId = TokenService.DecodeToken(token);
            List<Task> resultList = tasks.Where(t => t.User == UserId).ToList();
            return resultList;
        }

        public void Add(Task task)
        {
            task.Id = tasks.Max(t => t.Id)+1;
            tasks.Add(task);
            System.Console.WriteLine("in add task");
            saveToFile();
        }

        public bool Update(int id, Task task)
        {
            if (task.Id != id)
                return false;

            var task2 = tasks.FirstOrDefault(t => t.Id == id);
            task2.Name = task.Name;
            task2.TaskAccomplished = task.TaskAccomplished;
             System.Console.WriteLine("in update task");
            saveToFile();
            return true;
        }

        public bool Delete(int id,string userId)
        {
            List<Task> tasksList = tasks.Where(t=>t.User==userId).ToList();
            var task = tasksList.FirstOrDefault(p => p.Id == id);
            if (task == null)
                return false;
            tasks.Remove(task);
            saveToFile();
            return true;
        }
        public int Count => tasks.Count();
    }

}

using lesson1.Interfaces;
using System.Text.Json;



namespace lesson1.Controllers
{   
    public class TaskService : ITaskService
    {
        //private  ITaskService? taskService;
        private IWebHostEnvironment? webHost;
        private string filePath;
        private List<Task> tasks { get; } = new List<Task>();

        public TaskService(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
            this.filePath = Path.Combine(webHost.ContentRootPath, "Data", "db.json");
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
        // private  List<Task> tasks = new List<Task>
        // {
        //     new Task {Id=1, Name="HW #c", TaskAccomplished = false},
        //     new Task {Id=2, Name="HW angular", TaskAccomplished= true}
        // };

        public  List<Task> GetAll() {
            return tasks;
        }


        public  Task Get(int id)
        {
            return tasks.FirstOrDefault(p => p.Id == id);
        }

        public  void Add(Task task)
        {
            task.Id = tasks.Max(t => t.Id) + 1;
            tasks.Add(task);
        }

        public  bool Update(int id, Task task)
        {
            if (task.Id != id)
                return false;
             
            var task2 = tasks.FirstOrDefault(t => t.Id == id);
            task2.Name = task.Name;
            task2.TaskAccomplished = task.TaskAccomplished;
            return true;
        }

        public  bool Delete(int id)
        {
            var task = tasks.FirstOrDefault(p => p.Id == id);
            if (task == null)
                return false;
            tasks.Remove(task);
            return true;
        }
         public int Count => tasks.Count();
    }
          
}

using System.Collections.Generic;
using System.Linq;


namespace lesson1.Controllers
{   
    public static class TaskService
    {    
        private static List<Task> tasks = new List<Task>
        {
            new Task {Id=1, Name="HW #c", TaskAccomplished = false},
            new Task {Id=2, Name="HW angular", TaskAccomplished= true}
        };

        public static List<Task> GetAll() => tasks;


        public static Task Get(int id)
        {
            return tasks.FirstOrDefault(p => p.Id == id);
        }

        public static void Add(Task task)
        {
            task.Id = tasks.Max(t => t.Id) + 1;
            tasks.Add(task);
        }

        public static bool Update(int id, Task task)
        {
            if (task.Id != id)
                return false;
             
            var task2 = tasks.FirstOrDefault(t => t.Id == id);
            task2.Name = task.Name;
            task2.TaskAccomplished = task.TaskAccomplished;
            return true;
        }

        public static bool Delete(int id)
        {
            var task = tasks.FirstOrDefault(p => p.Id == id);
            if (task == null)
                return false;
            tasks.Remove(task);
            return true;
        }

    }
}
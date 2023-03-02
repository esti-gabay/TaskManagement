

namespace lesson1.Interfaces
{
    public interface ITaskService
    {
       public  List<Task> GetAll();

       public  Task Get(int id);

       public  void Add(Task task);

       public  bool Update(int id, Task task);

       public  bool Delete(int id);
    }
}


using lesson1.Interfaces;
using System.Text.Json;

namespace lesson1.Services
{
    public class UserService : IUserService
    {
        //private  ITaskService? taskService;
        private IWebHostEnvironment? webHost;
        private string filePath;
        private List<User> users { get; } = new List<User>();

        public UserService(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
            this.filePath = Path.Combine(webHost.ContentRootPath, "Data", "users.json");
            using (var jsonFile = File.OpenText(filePath))
            {
                users = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }

        private void saveToFile()
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(users));
        }

        public List<User> GetAll()
        {
            return users;
        }

        public User Get(string id)
        {
            return users.FirstOrDefault(p => p.Id == id);
        }

        public void Add(User user)
        {
            user.Id = (Int32.Parse(users.Max(u=>u.Id)!) + 1).ToString();
            users.Add(user);
            System.Console.WriteLine($"Add user: {user.Id}");
            saveToFile();
        }

        public bool Update(string id, User user)
        {
            if (user.Id != id)
                return false;

            var user2 = users.FirstOrDefault(t => t.Id == id);
            user2.UserName = user.UserName;
            user2.Password = user.Password;
            user2.Classification = user.Classification;
            System.Console.WriteLine($" Update user: {id}");
            saveToFile();
            return true;
        }

        public bool Delete(string id)
        {
            var user = users.FirstOrDefault(p => p.Id == id);
            if (user == null)
                return false;
            users.Remove(user);
            string userId = user.Id;
            System.Console.WriteLine($"Delete user: {userId}");

            saveToFile();
            return true;
        }

        public User Login(User user)
        {
            User tempUser = users.FirstOrDefault(u => u.Password == user.Password && u.UserName == user.UserName);
            return tempUser;

        }

        public void GetTokenId(string token)
        {

        }

    }

}
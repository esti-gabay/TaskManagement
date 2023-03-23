
namespace lesson1.Interfaces
{
    public interface IUserService
    {
        public List<User> GetAll();

        public User Get(string id);

        public void Add(User user);

        public bool Update(string id, User user);

        public bool Delete(string id);

        public User Login(User user);
    }
}

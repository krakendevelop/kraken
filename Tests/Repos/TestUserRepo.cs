using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Users;
using BusinessLogic.Users.Auth;

namespace Tests.Repos
{
  public class TestUserRepo : IUserRepo
  {
    private int _lastId;
    private readonly List<User> _users;

    public TestUserRepo()
    {
      _lastId = 1;
      _users = new List<User>();
      InitUsers();
    }

    private void InitUsers()
    {
      var user = new User(new AuthUser("user", "user@gmail.com", "user123", UserRole.User));
      user.SetId(_lastId++);
      _users.Add(user);

      var admin = new User(new AuthUser("admin", "admin@gmail.com", "admin123", UserRole.Admin));
      admin.SetId(_lastId++);
      _users.Add(admin);
    }

    public int Save(User user)
    {
      _users.Add(user);
      return user.Id;
    }

    public void Update(int id, User user)
    {
      throw new System.NotImplementedException();
    }

    public void Delete(User user)
    {
      throw new System.NotImplementedException();
    }

    public User Read(int id)
    {
      return _users.SingleOrDefault(u => u.Id == id);
    }
  }
}
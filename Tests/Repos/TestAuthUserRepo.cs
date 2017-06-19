using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Users.Auth;

namespace Tests.Repos
{
  public class TestAuthUserRepo : IAuthUserRepo
  {
    private int _lastId;
    private List<AuthUser> _authUsers;

    public TestAuthUserRepo()
    {
      _lastId = 1;
      _authUsers = new List<AuthUser>();
      InitUsers();
    }

    private void InitUsers()
    {
      var user = new AuthUser("user", "user@gmail.com", "user123", UserRole.User);
      user.SetId(_lastId++);

      var admin = new AuthUser("admin", "admin@gmail.com", "admin123", UserRole.Admin);
      admin.SetId(_lastId++);
      _authUsers.Add(user);
      _authUsers.Add(admin);
    }

    public int Save(AuthUser user)
    {
      user.SetId(_lastId++);
      _authUsers.Add(user);
      return user.Id;
    }

    public void Update(int id, AuthUser user)
    {
      throw new System.NotImplementedException();
    }

    public void Delete(AuthUser user)
    {
      throw new System.NotImplementedException();
    }

    public AuthUser ReadByEmail(string email)
    {
      return _authUsers.SingleOrDefault(u => u.Email == email);
    }

    public AuthUser ReadByUsermame(string username)
    {
      return _authUsers.SingleOrDefault(u => u.Username == username);
    }
  }
}
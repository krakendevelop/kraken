using System;

namespace BusinessLogic.Users.Auth
{
  public class AuthUser : BaseEntity
  {
    public string Username { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public DateTime RegistrationDate { get; private set; }
    public UserRole Role { get; private set; }

    public AuthUser(string username, string email, string password, UserRole role)
    {
      Username = username;
      Email = email;
      Password = password;
      Role = role;
    }

    public AuthUser(string username, string password)
    {
      Username = username;
      Password = password;
    }

    public AuthUser HideSensativeData()
    {
      Password = null;
      return this;
    }

    public AuthUser SetupNewlyRegistered()
    {
      RegistrationDate = DateTime.UtcNow;
      return this;
    }

    public bool IsPasswordMatch(string password)
    {
      return Password == password;
    }
  }
}
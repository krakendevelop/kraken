using System;

namespace BusinessLogic.Users.Auth
{
  public class AuthUser : BaseEntity
  {
    public string Username;
    public string Email;
    public string Password;
    public DateTime RegistrationDate;
    public UserRole Role;

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
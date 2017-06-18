using System;
using Common.Exceptions;

namespace BusinessLogic.Users.Auth
{
  public class AuthUser : IEntity
  {
    public int Id { get; private set; } // UserId is always equal to AuthUserId

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

    public AuthUser SetId(int id)
    {
      if (Id != 0)
        throw new KrakenException("Unable to set Id because it was not empty");

      Id = id;
      return this;
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
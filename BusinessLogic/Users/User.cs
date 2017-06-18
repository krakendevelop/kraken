using System;
using BusinessLogic.Users.Auth;

namespace BusinessLogic.Users
{
  public class User : AuthUser
  {
    public DateTime ProfileUpdateDate;

    public string ImageUrl;
    public string FirstName;
    public string LastName;
    public DateTime? BirthDate;
    public bool? Sex;
    public bool IsDeleted;

    public User(AuthUser authUser)
      : base(authUser.Username, authUser.Email, authUser.Password, authUser.Role)
    {
      HideSensativeData();
    }
  }
}
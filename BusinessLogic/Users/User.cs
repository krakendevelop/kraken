using System;
using BusinessLogic.Users.Auth;

namespace BusinessLogic.Users
{
  public class User : AuthUser
  {
    public DateTime ProfileUpdateDate;

    public string ImageUrl { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateTime? BirthDate { get; private set; }
    public bool? Sex { get; private set; }
    public bool IsDeleted { get; private set; }

    public User(AuthUser authUser, string imageUrl, string firstName, string lastName, DateTime? birthDate, bool? sex)
      : base(authUser.Username, authUser.Email, authUser.Password, authUser.Role)
    {
      ImageUrl = imageUrl;
      FirstName = firstName;
      LastName = lastName;
      BirthDate = birthDate;
      Sex = sex;

      ProfileUpdateDate = DateTime.UtcNow;

      HideSensativeData();
    }
  }
}
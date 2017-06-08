using System;
using Common.Exceptions;

namespace BusinessLogic.Users
{
  public class UserManager
  {
    public User Create(string login, string email, string password, string imageUrl,
      string firstName, string lastName, DateTime? birthDate, bool? sex)
    {
      var user = new User
      {
        Login = login,
        Email = email,
        Password = password,
        ImageUrl = imageUrl,
        RegistrationDate = DateTime.UtcNow,
        ProfileUpdateDate = DateTime.UtcNow,

        FirstName = firstName,
        LastName = lastName,
        BirthDate = birthDate,
        Sex = sex
      };

      user.Id = Repositories.Users.Save(user);
      return user;
    }

    public User Get(int id)
    {
      var user = Repositories.Users.Read(id);

      if (user == null)
        throw new KrakenException(KrakenExceptionCode.User_NotFound, "Unable to find user with Id " + id);

      return user;
    }
  }
}
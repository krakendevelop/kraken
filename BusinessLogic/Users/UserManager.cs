using System;
using BusinessLogic.Users.Auth;
using Common.Exceptions;

namespace BusinessLogic.Users
{
  public class UserManager
  {
    private readonly IUserRepo _userRepo;

    public UserManager(IUserRepo userRepo)
    {
      _userRepo = userRepo;
    }

    public User Create(AuthUser authUser, string imageUrl,
      string firstName, string lastName, DateTime? birthDate, bool? sex)
    {
      var user = new User(authUser, imageUrl, firstName, lastName, birthDate, sex);

      var id = _userRepo.Save(user);
      user.SetId(id);

      return user;
    }

    public User Get(int id)
    {
      var user = _userRepo.Read(id);

      if (user == null)
        throw new KrakenException(KrakenExceptionCode.User_NotFound, "Unable to find user with Id " + id);

      return user;
    }
  }
}
using Common.Exceptions;
using Common.Serialization;
using log4net;

namespace BusinessLogic.Users.Auth
{
  public class AuthUserManager
  {
    private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private readonly IAuthUserRepo _repo;

    public AuthUserManager(IAuthUserRepo repo)
    {
      _repo = repo;
    }

    public AuthUser Register(string username, string email, string password, UserRole role)
    {
      var authUser = new AuthUser(username, email, password, role)
        .SetupNewlyRegistered();

      var id = _repo.Save(authUser);
      authUser.SetId(id);

      Logger.DebugFormat("Registed new auth user: {0}", authUser.ToJson());

      return authUser;
    }

    public AuthenticationStatus ValidateAndRead(string password, out AuthUser authUser, string username = null, string email = null)
    {
      if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(email))
        throw new KrakenException("Unable to validate user with empty email and username fields");

      AuthUser existingUser;
      authUser = null;
      if (!string.IsNullOrEmpty(email))
      {
        existingUser = _repo.ReadByEmail(email);
        if (existingUser == null)
          return AuthenticationStatus.UserNotFound;

        authUser = existingUser;
        return existingUser.IsPasswordMatch(password)
          ? AuthenticationStatus.Succesful
          : AuthenticationStatus.PasswordDoesNotMatch;
      }

      existingUser = _repo.ReadByUsermame(username);
      if (existingUser == null)
        return AuthenticationStatus.UserNotFound;

      authUser = existingUser;
      return existingUser.IsPasswordMatch(password)
        ? AuthenticationStatus.Succesful
        : AuthenticationStatus.PasswordDoesNotMatch;
    }
  }
}
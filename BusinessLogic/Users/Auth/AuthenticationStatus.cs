namespace BusinessLogic.Users.Auth
{
  public enum AuthenticationStatus
  {
    PasswordDoesNotMatch = -2,
    UserNotFound = -1,
    Unknown = 0,
    Successful = 1,
  }
}
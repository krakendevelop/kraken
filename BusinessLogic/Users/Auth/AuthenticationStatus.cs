namespace BusinessLogic.Users.Auth
{
  public enum AuthenticationStatus
  {
    PasswordDoesNotMatch = -2,
    UserNotFound = -1,
    Unkown = 0,
    Succesful = 1,
  }
}
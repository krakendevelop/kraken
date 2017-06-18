namespace BusinessLogic.Users.Auth
{
  public class AuthUserRepo : IAuthUserRepo
  {
    public int Save(AuthUser user)
    {
      throw new System.NotImplementedException();
    }

    public void Update(int id, AuthUser user)
    {
      throw new System.NotImplementedException();
    }

    public void Delete(AuthUser user)
    {
      throw new System.NotImplementedException();
    }
    
    public AuthUser ReadByEmail(string email)
    {
      throw new System.NotImplementedException();
    }

    public AuthUser ReadByUsermame(string username)
    {
      throw new System.NotImplementedException();
    }
  }
}
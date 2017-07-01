namespace BusinessLogic.Users
{
  public interface IUserRepo : IRepo
  {
    int Save(User user);
    void Update(int id, User user);
    void Delete(User user);
    User Read(int id);
  }
}
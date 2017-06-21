using BusinessLogic.Users;

namespace WebApp.Models
{
  public class PartialUserModel
  {
    public string ImageUrl { get; private set; }
    public string Username { get; private set; }

    public PartialUserModel(User user)
    {
      ImageUrl = user.ImageUrl;
      Username = user.Username;
    }
  }
}
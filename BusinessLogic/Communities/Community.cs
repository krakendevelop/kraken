using System;

namespace BusinessLogic.Communities
{
  public class Community : BaseEntity
  {
    public int OwnerUserId { get; private set; }
    public string Name { get; private set; }
    public string PictureUrl { get; private set; }
    public DateTime CreateTime { get; private set; }
    public DateTime LastUpdateTime { get; private set; }

    public Community(int ownerUserId, string name, string pictureUrl)
    {
      OwnerUserId = ownerUserId;
      Name = name;
      PictureUrl = pictureUrl;

      CreateTime = DateTime.UtcNow;
      LastUpdateTime = DateTime.UtcNow;
    }
  }
}
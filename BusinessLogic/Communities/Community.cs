using System;

namespace BusinessLogic.Communities
{
  // obsolete feature
  [Obsolete]
  public class Community : BaseEntity
  {
    public int OwnerUserId { get; private set; }
    public string Name { get; private set; }
    public string ImageUrl { get; private set; }
    public DateTime CreateTime { get; private set; }
    public DateTime UpdateTime { get; private set; }
    public bool IsDeleted { get; private set; }

    public Community(int ownerUserId, string name, string imageUrl)
    {
      OwnerUserId = ownerUserId;
      Name = name;
      ImageUrl = imageUrl;

      CreateTime = DateTime.UtcNow;
      UpdateTime = DateTime.UtcNow;
    }

    public Community Delete()
    {
      IsDeleted = true;
      UpdateTime = DateTime.UtcNow;
      return this;
    }
  }
}
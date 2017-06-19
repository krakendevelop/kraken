using Common.Exceptions;

namespace BusinessLogic
{
  public abstract class BaseEntity
  {
    public int Id { get; private set; }

    public void SetId(int id)
    {
      if (Id != 0)
        throw new KrakenException("Unable to set Id because it was not empty");

      Id = id;
    }
  }
}

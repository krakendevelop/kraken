using System;

namespace BusinessLogic.Users
{
  public class User : IEntity
  {
    public int Id { get; set; }

    public string Username;
    public string Email;
    public string Password;
    public string ImageUrl;
    public DateTime RegistrationDate;
    public DateTime ProfileUpdateDate;

    public string FirstName;
    public string LastName;
    public DateTime? BirthDate;
    public bool? Sex;
    public bool IsDeleted;
  }
}
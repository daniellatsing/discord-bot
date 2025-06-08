namespace BirthdayBot.Models
{
  public class UserBirthday
  {
    public int Id { get; set; }
    public ulong UserId { get; set; }
    public DateTime Birthday { get; set; }
    public string TimeZone { get; set; } = "UTC";
  }
}
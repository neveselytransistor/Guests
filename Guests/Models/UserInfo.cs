namespace Guests.Models
{
    public class UserInfo
    {
        public int UserInfoId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public UserState State { get; set; }
    }
}
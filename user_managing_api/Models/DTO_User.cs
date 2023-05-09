namespace user_managing_api.Models
{
    public class DTO_User
    {
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public UInt32? User_Group_Id { get; set; }
    }
}

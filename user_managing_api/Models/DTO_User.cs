namespace user_managing_api.Models
{
    public class DTO_User
    {
        public UInt32 Id { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public UInt32? User_Group_Id { get; set; }
        public UInt32? User_State_Id { get; set; }
    }
}

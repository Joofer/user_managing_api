namespace user_managing_api.Models
{
    public class User_Group
    {
        public UInt32 Id { get; set; }
        public string Code { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual List<User> Users { get; set; } = new List<User>();
    }
}

using System.Text.Json.Serialization;

namespace user_managing_api.Models
{
    public class User_Group
    {
        public UInt32 Id { get; set; }
        public string Code { get; set; } = null!;
        public string Description { get; set; } = null!;

        [JsonIgnore] public virtual ICollection<User>? Users { get; set; }
    }
}

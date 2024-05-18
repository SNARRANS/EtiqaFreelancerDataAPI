using System.ComponentModel.DataAnnotations;

namespace EtiqaFreelancerDataAPI.Models
{
    public class Profile
    {
        [Key]
        public required int Id { get; set; }
        public required string Username { get; set; }
        public required string Mail { get; set; }
        public string PhoneNo { get; set; } = string.Empty;
        public string? SkillSet { get; set; } = string.Empty;
        public string? Hobby { get; set; } = string.Empty;
    }


}

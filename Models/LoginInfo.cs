using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EtiqaFreelancerDataAPI.Models
{
    public class LoginInfo
    {
      
        public required string Username { get; set; }
        public required string Userpassword { get; set; }
    }
}

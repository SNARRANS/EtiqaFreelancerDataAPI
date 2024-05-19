using EtiqaFreelancerDataAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EtiqaFreelancerDataAPI.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
          : base(dbContextOptions)
        {

        }

        public DbSet<LoginInfo> LoginInfos { get; set; }

        public DbSet<Profile> Profiles { get; set; }
        
    }

}

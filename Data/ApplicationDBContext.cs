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

        public DbSet<Profile> Profiles { get; set; }
        
    }

}

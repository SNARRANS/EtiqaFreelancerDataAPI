using EtiqaFreelancerDataAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EtiqaFreelancerDataAPI.Data
{
    public interface IFreelancerRepository
    {
        Task<IEnumerable<Profile>> GetProfiles();
        Task<Profile> InsertProfile(Profile objProfile);
    }
    public class FreelancerRepository : IFreelancerRepository
    {
        private ApplicationDBContext _dbContext;

        public FreelancerRepository(ApplicationDBContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Profile>> GetProfiles()
        {
            return await _dbContext.Profiles.ToListAsync();
        }

        public async Task<Profile> InsertProfile(Profile objProfile)
        {
            _dbContext.Profiles.Add(objProfile);
            await _dbContext.SaveChangesAsync();
            return objProfile;

        }

    }
}

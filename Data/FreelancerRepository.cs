using EtiqaFreelancerDataAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace EtiqaFreelancerDataAPI.Data
{
    public interface IFreelancerRepository
    {
        Task<IEnumerable<Profile>> GetProfiles();
        Task<PaginatedList<Profile>> GetProfilesV2(int pageIndex, int pageSize);
        Task<Profile?> GetProfileById(int Id);
        Task<Profile> InsertProfile(Profile objProfile);
        Task<Profile> UpdateProfile(Profile objProfile);
        bool DeleteProfile(int Id);
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

        public async Task<PaginatedList<Profile>> GetProfilesV2(int pageIndex, int pageSize)
        {
            var profiles = await _dbContext.Profiles
                .OrderBy(b => b.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var count = await _dbContext.Profiles.CountAsync();
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            return new PaginatedList<Profile>(profiles, pageIndex, totalPages);
        }

        public async Task<Profile?> GetProfileById(int Id)
        {
            return await _dbContext.Profiles.FindAsync(Id);

        }

        public async Task<Profile> InsertProfile(Profile objProfile)
        {
            _dbContext.Profiles.Add(objProfile);
            await _dbContext.SaveChangesAsync();
            return objProfile;

        }

        public async Task<Profile> UpdateProfile(Profile objProfile)
        {
            _dbContext.Entry(objProfile).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return objProfile;

        }

        public bool DeleteProfile(int Id)
        {
            bool result = false;
            var profile = _dbContext.Profiles.Find(Id);
            if (profile != null)
            {
                _dbContext.Entry(profile).State = EntityState.Deleted;
                _dbContext.SaveChanges();
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }



    }
}

using EtiqaFreelancerDataAPI.Data;
using EtiqaFreelancerDataAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace EtiqaFreelancerDataAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FreelancerController : ControllerBase
    {
        private readonly IFreelancerRepository _freelancer;
        private readonly IMemoryCache _memoryCache;
        //private readonly IDistributedCache _distributedCache;

        public FreelancerController(IFreelancerRepository freelancer, IMemoryCache memoryCache)
        {
            _freelancer = freelancer ?? throw new ArgumentNullException(nameof(freelancer));
            _memoryCache = memoryCache;

            //_distributedCache = distributedCache;
        }

        [HttpGet]
        [Route("GetProfiles")]
        public async Task<IActionResult> Get()
        {            
            return Ok(await _freelancer.GetProfiles());
        }

      
        [HttpGet]
        [Route("GetProfilesV2")]
        public async Task<IActionResult> Get2()
        {
            var cacheData = _memoryCache.Get<IEnumerable<Profile>>("Profiles");
            if (cacheData != null)
            {
                return Ok(cacheData);
            }

            var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
            cacheData = await _freelancer.GetProfiles();
            _memoryCache.Set("Profiles", cacheData, expirationTime);

            return Ok(cacheData);
        }

        [HttpGet]
        [Route("GetProfilesV3")]
        public async Task<ActionResult<ApiResponse>> Get3(int pageIndex = 1, int pageSize = 10)
        {
            var players = await _freelancer.GetProfilesV3(pageIndex, pageSize);
            return new ApiResponse(true, null, players);
        }


        [HttpGet]
        [Route("GetProfileById/{Id}")]
        public async Task<IActionResult> GetProfileById(int Id)
        {
            return Ok(await _freelancer.GetProfileById(Id));
        }

        [HttpPost]
        [Route("AddProfile")]
        public async Task<IActionResult> Post(Profile obj)
        {
            var result = await _freelancer.InsertProfile(obj);
            if (result.Id == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return Ok("Added Successfully");
        }

        [HttpPut]
        [Route("UpdateProfile")]
        public async Task<IActionResult> Put(Profile obj)
        {
            await _freelancer.UpdateProfile(obj);
            return Ok("Updated Successfully");
        }

        //[HttpDelete]
        ////[HttpDelete("{id}")]
        //[Route("DeleteProfile")]
        //public JsonResult Delete(int id)
        //{
        //    _freelancer.DeleteProfile(id);
        //    return new JsonResult("Deleted Successfully");
        //}


        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int Id)
        {
            if (!_freelancer.DeleteProfile(Id))
            {
                return NotFound();
            }

            return Ok(new
            {
                message = "Deleted Successfully",
                Id = Id
            });
        }




    }
}

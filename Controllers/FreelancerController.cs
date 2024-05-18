using EtiqaFreelancerDataAPI.Data;
using EtiqaFreelancerDataAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EtiqaFreelancerDataAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FreelancerController : ControllerBase
    {
        private readonly IFreelancerRepository _freelancer;

        public FreelancerController(IFreelancerRepository freelancer)
        {
            _freelancer = freelancer ?? throw new ArgumentNullException(nameof(freelancer));
        }

        [HttpGet]
        [Route("GetProfiles")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _freelancer.GetProfiles());
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

        [HttpDelete]
        //[HttpDelete("{id}")]
        [Route("DeleteProfile")]
        public JsonResult Delete(int id)
        {
            _freelancer.DeleteProfile(id);
            return new JsonResult("Deleted Successfully");
        }




    }
}

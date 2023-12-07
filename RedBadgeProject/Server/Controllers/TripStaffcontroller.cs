using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RedBadgeProject.Server.Services.TripStaff;
using RedBadgeProject.Shared.Models.TripStaff;

namespace RedBadgeProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripStaffcontroller : ControllerBase
    {
        private readonly ITripStaffService _tripStaffService;
        public TripStaffcontroller(ITripStaffService tripStaffService)
        {
            _tripStaffService = tripStaffService;
        }

        private string GetUserId()
        {
            string userIdClaim = User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;

            if (userIdClaim == null) return null!;
                return userIdClaim;
        }

        private bool SetUserIdInService()
        {
            var userId = GetUserId();
            if (userId == null) return false;

            _tripStaffService.SetUserId(userId);
            return true;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTripStaffById(int id)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (!SetUserIdInService()) return Unauthorized();

            var tripStaff = await _tripStaffService.GetTripStaffByIdAsync(id);

            if (tripStaff == null) return NotFound();

            return Ok(tripStaff);
        }

        [HttpGet("sg/{staffId}")]
        public async Task<List<TripStaffDetail>> StaffGroup(int staffId)
        {
            List<TripStaffDetail> group = await _tripStaffService.GetAllTripStaffByStaffIdAsync(staffId);
                return group.ToList();
        }

        [HttpGet("tg/{tripId}")]
        public async Task<List<TripStaffDetail>> TripGroup(int tripId)
        {
            List<TripStaffDetail> group = await _tripStaffService.GetAllTripStaffByTripIdAsync(tripId);
                return group.ToList();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TripStaffCreate model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (!SetUserIdInService()) return Unauthorized();

            bool wasSuccessful = await _tripStaffService.CreateTripStaffAsync(model);

            if (wasSuccessful) return Ok();
            else return UnprocessableEntity();
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, TripStaffEdit model)
        {
            if (!SetUserIdInService()) return Unauthorized();

            if (model == null || !ModelState.IsValid || model.Id != id) return BadRequest();

            bool wasSuccessful = await _tripStaffService.UpdateTripStaffAsync(model);

            if (wasSuccessful) return Ok();
                return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            
            var tripStaff = await _tripStaffService.GetTripStaffByIdAsync(id);

            if(tripStaff == null) return NotFound();

            bool wasSuccessful = await _tripStaffService.DeleteTripStaffAsync(id);

            if (!wasSuccessful) return BadRequest();
                return Ok();
        }
    }
}
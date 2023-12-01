using Microsoft.AspNetCore.Mvc;
using RedBadgeProject.Shared.Models.Trip;
using RedBadgeProject.Services.Trip;
using System.Security.Claims;

namespace RedBadgeProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly ITripService _tripService;
        public TripController(ITripService tripService)
        {
            _tripService = tripService;
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

            _tripService.SetUserId(userId);
            return true;
        }

        [HttpGet]
        public async Task<List<TripDetail>> Index()
        {
            if (!SetUserIdInService()) return new List<TripDetail>();

            List<TripDetail> trips = await _tripService.GetAllTripsAsync();
            return trips.ToList();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Trip(int id)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (!SetUserIdInService()) return Unauthorized();

            var trip = await _tripService.GetTripByIdAsync(id);

            if (trip == null) return NotFound();

            return Ok(trip);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TripCreate model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (!SetUserIdInService()) return Unauthorized();

            bool wasSuccessful = await _tripService.CreateTripAsync(model);

            if (wasSuccessful) return Ok();
            else return UnprocessableEntity();
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, TripEdit model)
        {
            if (!SetUserIdInService()) return Unauthorized();
            
            if (model == null || !ModelState.IsValid || model.Id != id) return BadRequest();

            bool wasSuccessful = await _tripService.UpdateTripAsync(model);

            if (wasSuccessful) return Ok();
                return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            
            var trip = await _tripService.GetTripByIdAsync(id);

            if (trip == null) return NotFound();

            bool wasSuccessful = await _tripService.DeleteTripAsync(id);

            if (!wasSuccessful) return BadRequest();
            return Ok();
        }
    }
}
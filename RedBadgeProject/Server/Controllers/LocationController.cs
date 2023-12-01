using Microsoft.AspNetCore.Mvc;
using RedBadgeProject.Shared.Models.Location;
using RedBadgeProject.Services.Location;
using System.Security.Claims;

namespace RedBadgeProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;
        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
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

            _locationService.SetUserId(userId);
            return true;
        }

        [HttpGet]
        public async Task<List<LocationDetail>> Index()
        {
            if (!SetUserIdInService()) return new List<LocationDetail>();

            List<LocationDetail> locations = await _locationService.GetAllLocationsAsync();
            return locations.ToList();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Location(int id)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (!SetUserIdInService()) return Unauthorized();

            var location = await _locationService.GetLocationByIdAsync(id);

            if (location == null)
            {
                return NotFound();
            }
            return Ok(location);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LocationCreate model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (!SetUserIdInService()) return Unauthorized();

            bool wasSuccessful = await _locationService.CreateLocationAsync(model);

            if (wasSuccessful) return Ok();
            else return UnprocessableEntity();
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, LocationEdit model)
        {
            if (!SetUserIdInService()) return Unauthorized();
            
            if (model == null || !ModelState.IsValid || model.Id != id) return BadRequest();

            bool wasSuccessful = await _locationService.UpdateLocationAsync(model);

            if (wasSuccessful)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var location = await _locationService.GetLocationByIdAsync(id);

            if (location == null) return NotFound();

            bool wasSuccessful = await _locationService.DeleteLocationAsync(id);

            if (!wasSuccessful) return BadRequest();
                return Ok();
        }
    }
}
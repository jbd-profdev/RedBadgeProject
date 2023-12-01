using Microsoft.AspNetCore.Mvc;
using RedBadgeProject.Shared.Models.Vehicle;
using RedBadgeProject.Services.Vehicle;
using System.Security.Claims;

namespace RedBadgeProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
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

            _vehicleService.SetUserId(userId);
            return true;
        }

        [HttpGet]
        public async Task<List<VehicleDetail>> Index()
        {
            if (!SetUserIdInService()) return new List<VehicleDetail>();

            List<VehicleDetail> vehicles = await _vehicleService.GetAllVehiclesAsync();
            return vehicles.ToList();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Vehicle(int id)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (!SetUserIdInService()) return Unauthorized();

            var vehicle = await _vehicleService.GetVehicleByIdAsync(id);

            if (vehicle == null) return NotFound();

            return Ok(vehicle);
        }

        [HttpPost]
        public async Task<IActionResult> Create(VehicleCreate model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (!SetUserIdInService()) return Unauthorized();

            bool wasSuccessful = await _vehicleService.CreateVehicleAsync(model);

            if (wasSuccessful) return Ok();
            else return UnprocessableEntity();
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, VehicleEdit model)
        {
            if (!SetUserIdInService()) return Unauthorized();

            if (model == null || !ModelState.IsValid || model.Id != id) return BadRequest();

            bool wasSuccessful = await _vehicleService.UpdateVehicleAsync(model);

            if (wasSuccessful) return Ok();
                return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var vehicle = await _vehicleService.GetVehicleByIdAsync(id);

            if (vehicle == null) return NotFound();

            bool wasSuccessful = await _vehicleService.DeleteVehicleAsync(id);

            if (!wasSuccessful) return BadRequest();
            return Ok();
        }
    }
}
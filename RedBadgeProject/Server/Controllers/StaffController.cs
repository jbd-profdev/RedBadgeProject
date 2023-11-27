using Microsoft.AspNetCore.Mvc;
using RedBadgeProject.Shared.Models.Staff;
using RedBadgeProject.Services.Staff;
using System.Security.Claims;

namespace RedBadgeProject.Shared.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;
        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
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

            _staffService.SetUserId(userId);
            return true;
        }

        [HttpGet]
        public async Task<List<StaffDetail>> Index()
        {
            if (!SetUserIdInService()) return new List<StaffDetail>();

            List<StaffDetail> staff = await _staffService.GetAllStaffAsync();
                return staff.ToList();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Staff(int id)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (!SetUserIdInService()) return Unauthorized();

            var staff = await _staffService.GetStaffByIdAsync(id);

            if (staff == null) return NotFound();

            return Ok(staff);
        }

        [HttpPost]
        public async Task<IActionResult> Create(StaffCreate model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (!SetUserIdInService()) return Unauthorized();

            bool wasSuccessful = await _staffService.CreateStaffAsync(model);

            if (wasSuccessful) return Ok();
            else return UnprocessableEntity();
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, StaffEdit model)
        {
            if (!SetUserIdInService()) return Unauthorized();

            if (model == null || !ModelState.IsValid || model.Id != id) return BadRequest();

            bool wasSuccessful = await _staffService.UpdateStaffAsync(model);

            if (wasSuccessful) return Ok();
                return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, StaffDelete staffId)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var staff = await _staffService.GetStaffByIdAsync(id);

            if (staff == null) return NotFound();

            bool wasSuccessful = await _staffService.DeleteStaffAsync(staffId);

            if (!wasSuccessful) return BadRequest();
            return Ok();
        }
    }
}
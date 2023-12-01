using Microsoft.AspNetCore.Mvc;
using RedBadgeProject.Shared.Models.Company;
using RedBadgeProject.Services.Company;
using System.Security.Claims;

namespace RedBadgeProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
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

            _companyService.SetUserId(userId);
            return true;
        }

        [HttpGet]
        public async Task<List<CompanyDetail>> Index()
        {
            if (!SetUserIdInService()) return new List<CompanyDetail>();

            List<CompanyDetail> companies = await _companyService.GetAllCompaniesAsync();
            return companies.ToList();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Company(int id)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (!SetUserIdInService()) return Unauthorized();

            var company = await _companyService.GetCompanyByIdAsync(id);

            if (company == null) return NotFound();

            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CompanyCreate model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (!SetUserIdInService()) return Unauthorized();

            bool wasSuccessful = await _companyService.CreateCompanyAsync(model);

            if (wasSuccessful) return Ok();
            else return UnprocessableEntity();
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, CompanyEdit model)
        {
            if (!SetUserIdInService()) return Unauthorized();
            
            if (model == null || !ModelState.IsValid || model.Id != id) return BadRequest();

            bool wasSuccessful = await _companyService.UpdateCompanyAsync(model);

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
            
            var company = await _companyService.GetCompanyByIdAsync(id);

            if (company == null) return NotFound();

            bool wasSuccessful = await _companyService.DeleteCompanyAsync(id);

            if (!wasSuccessful) return BadRequest();
            return Ok();
        }
    }
}
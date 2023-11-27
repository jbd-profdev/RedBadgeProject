using Microsoft.EntityFrameworkCore;
using RedBadgeProject.Server.Data;
using RedBadgeProject.Server.Models;
using RedBadgeProject.Shared.Models.Company;

namespace RedBadgeProject.Services.Company
{
    public class CompanyService : ICompanyService
    {
        private readonly ApplicationDbContext _dbcontext;
        private string _userId;
        public void SetUserId(string userId) => _userId = userId;

        public CompanyService(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task<bool> CreateCompanyAsync(CompanyCreate request)
        {
            CompanyEntity entity = new()
            {
                Name = request.Name,
                LocationId = request.LocationId
            };

            _dbcontext.Companies.Add(entity);
            var numberOfChanges = await _dbcontext.SaveChangesAsync();

            if (numberOfChanges == 1)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteCompanyAsync(CompanyDelete companyId)
        {
            var companyEntity = await _dbcontext.Companies.FindAsync(companyId.Id);

            if (companyEntity is null)
                return false;

            _dbcontext.Companies.Remove(companyEntity);

            return await _dbcontext.SaveChangesAsync() == 1;
        }

        public async Task<List<CompanyDetail>> GetAllCompaniesAsync()
        {
            List<CompanyDetail> companies = await _dbcontext.Companies.Select(entity => new CompanyDetail
            {
                Id = entity.Id,
                Name = entity.Name,
                LocationId = entity.LocationId
            }).ToListAsync();

            return companies;
        }

        public async Task<CompanyDetail?> GetCompanyByIdAsync(int companyId)
        {
            CompanyEntity? entity = await _dbcontext.Companies.FindAsync(companyId);
            return entity is null ? null : new CompanyDetail
            {
                Id = entity.Id,
                Name = entity.Name,
                LocationId = entity.LocationId
            };
        }

        public async Task<bool> UpdateCompanyAsync(CompanyEdit request)
        {
            CompanyEntity? entity = await _dbcontext.Companies.FindAsync(request.Id);

            if (entity is null)
                return false;

            entity.Name = request.Name;
            entity.LocationId = request.LocationId;

            var numberOfChanges = await _dbcontext.SaveChangesAsync();

            return numberOfChanges == 1;
        }
    }
}
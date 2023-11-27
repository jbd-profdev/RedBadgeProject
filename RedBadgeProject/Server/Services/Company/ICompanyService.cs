using RedBadgeProject.Shared.Models.Company;

namespace RedBadgeProject.Services.Company
{
    public interface ICompanyService
    {
        void SetUserId(string userId);
        Task<bool> CreateCompanyAsync(CompanyCreate request);
        Task<List<CompanyDetail>> GetAllCompaniesAsync();
        Task<CompanyDetail?> GetCompanyByIdAsync(int companyId);
        Task<bool> UpdateCompanyAsync(CompanyEdit request);
        Task<bool> DeleteCompanyAsync(CompanyDelete companyId);
    }
}
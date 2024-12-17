using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface ITaxService
    {
        public Task<Guid> AddTax(double taxPercentage);
        public Task<Guid> UpdateTax(TaxDto updateTax);
        public Task<TaxDto> GetTax();
    }
}

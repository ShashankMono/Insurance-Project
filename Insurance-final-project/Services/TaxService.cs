using AutoMapper;
using Insurance_final_project.Dto;
using Insurance_final_project.Exceptions;
using Insurance_final_project.Models;
using Insurance_final_project.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Insurance_final_project.Services
{
    public class TaxService : ITaxService
    {
        private readonly IRepository<Tax> _taxRepo;
        private readonly IMapper _mapper;

        public TaxService(IRepository<Tax> taxRepo,IMapper mapper)
        {
            _taxRepo = taxRepo;
            _mapper = mapper;
        }
        public async Task<Guid> AddTax(double taxPercentage)
        {
            if (taxPercentage < 0) {
                throw new ArgumentException("Invalid Tax Percentage");
            }
            Tax tax = new Tax() { TaxPercentage = Math.Round( taxPercentage , 2) };
            return _taxRepo.Add(tax).TaxId;
        }

        public async Task<TaxDto> GetTax()
        {
            var tax = _taxRepo.GetAll().AsNoTracking().First();
            return _mapper.Map<TaxDto>(tax);
        }

        public async Task<Guid> UpdateTax(TaxDto updateTax)
        {
            var existingTax = _taxRepo.GetAll().AsNoTracking().FirstOrDefault(t=>t.TaxId == updateTax.TaxId);
            if (updateTax.TaxPercentage < 0)
            {
                throw new ArgumentException("Invalid Tax Percentage");
            }
            if (existingTax == null) {
                throw new InvalidGuidException("Tax entry not found!");
            }

            existingTax.TaxPercentage = Math.Round(updateTax.TaxPercentage, 2);
            return _taxRepo.Update(existingTax).TaxId;
        }
    }
}

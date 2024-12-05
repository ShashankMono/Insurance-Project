using AutoMapper;
using Insurance_final_project.Dto;
using Insurance_final_project.Exceptions;
using Insurance_final_project.Models;
using Insurance_final_project.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Insurance_final_project.Services
{
    public class NomineeService : INomineeService
    {
        private IRepository<Nominee> _nomineeRepo;
        private IMapper _mapper;
        public NomineeService(IRepository<Nominee>repo,IMapper mapper)
        {
            _nomineeRepo = repo;
            _mapper = mapper;
        }
        public async Task<Guid> AddNominee(NomineeDto nominee)
        {
            return _nomineeRepo.Add(_mapper.Map<Nominee>(nominee)).Id;
        }

        public async Task<NomineeDto> GetNominee(Guid nomineeId)
        {
            var nominee = _nomineeRepo.Get(nomineeId);
            if (nominee == null) {
                throw new InvalidGuidException("Invalid Nominee!");
            }
            return _mapper.Map<NomineeDto>(nominee);
        }

        public async Task<List<NomineeDto>> GetNominees(Guid customerId)
        {
            var nominees = _nomineeRepo.GetAll().Where(n=>n.CustomerId == customerId).ToList();
            if (nominees.Count ==0)
            {
                throw new NoNomineePresentException("No Nominee found!");
            }
            return _mapper.Map<List<NomineeDto>>(nominees);
        }

        public async Task<Guid> UpdateNominee(NomineeDto nominee)
        {
             
            if (_nomineeRepo.GetAll().AsNoTracking().FirstOrDefault(n=> n.Id ==nominee.Id) == null)
            {
                throw new InvalidGuidException("Invalid Nominee!");
            }
            return _nomineeRepo.Update(_mapper.Map<Nominee>(nominee)).Id;
        }

        public async Task<bool> Delete(Guid nomineeId)
        {
            var nominee = _nomineeRepo.GetAll().AsNoTracking().FirstOrDefault(n => n.Id == nomineeId);
            if ( nominee== null)
            {
                throw new InvalidGuidException("Invalid Nominee!");
            }
            nominee.IsActive = false;
            _nomineeRepo.Update(nominee);
            return true;
        }
    }
}

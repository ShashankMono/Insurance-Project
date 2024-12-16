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
        private IRepository<Customer> _customerRepo;
        private IRepository<PolicyAccount> _PolicyAccountRepo;
        public NomineeService(IRepository<Nominee>repo,
            IMapper mapper,
            IRepository<Customer> cutomerRepo,
            IRepository<PolicyAccount> policyAccount)
        {
            _nomineeRepo = repo;
            _mapper = mapper;
            _customerRepo = cutomerRepo;
            _PolicyAccountRepo = policyAccount;
        }
        public async Task<Guid> AddNominee(NomineeDto nominee)
        {
            check(nominee.CustomerId,nominee.PolicyAccountId);
            if (_nomineeRepo.GetAll().AsNoTracking()
                .Where(n => n.CustomerId == nominee.CustomerId &&
                n.NomineeName == nominee.NomineeName &&
                n.NomineeRelation == nominee.NomineeRelation &&
                n.PolicyAccountId == nominee.PolicyAccountId).FirstOrDefault() != null)
            {
                throw new NomineeAlreadyExistException("Nominee already exist!");
            }
            return _nomineeRepo.Add(_mapper.Map<Nominee>(nominee)).Id;
        }

        public void check(Guid customerId, Guid policyAccountId)
        {
            if (_customerRepo.Get(customerId) == null)
                throw new CustomerNotFoundException("Customer not found!");
            if(_PolicyAccountRepo.Get(policyAccountId) == null)
            {
                throw new PolicyAccountNotFountException("Account not found!");
            }
        }
        public async Task<NomineeDto> GetNominee(Guid nomineeId)
        {
            var nominee = _nomineeRepo.Get(nomineeId);
            if (nominee == null) {
                throw new InvalidGuidException("Invalid Nominee!");
            }
            return _mapper.Map<NomineeDto>(nominee);
        }

        public async Task<List<NomineeDto>> GetNominees(Guid customerId,Guid PolicyAccountId)
        {
            check(customerId,PolicyAccountId);
            var nominees = _nomineeRepo.GetAll().AsNoTracking().Where(n=>n.CustomerId == customerId &&
            n.PolicyAccountId == PolicyAccountId).ToList();
            if (nominees.Count ==0)
            {
                throw new NoNomineePresentException("No Nominee found!");
            }
            return _mapper.Map<List<NomineeDto>>(nominees);
        }

        public async Task<Guid> UpdateNominee(NomineeDto nominee)
        {
             check(nominee.CustomerId,nominee.PolicyAccountId);
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
            _nomineeRepo.Delete(nominee);
            return true;
        }
    }
}

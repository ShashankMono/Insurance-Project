using AutoMapper;
using Insurance_final_project.Dto;
using Insurance_final_project.Exceptions;
using Insurance_final_project.Models;
using Insurance_final_project.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Insurance_final_project.Services
{
    public class PolicyService:IPolicyService
    {
        private readonly IRepository<Policy> _PolicyRepo;
        private readonly IMapper _Mapper;
        private readonly IRepository<PolicyType> _typeRepo;
        public PolicyService(IRepository<Policy> repo, IMapper mapper,IRepository<PolicyType> type)
        {
            _PolicyRepo = repo;
            _Mapper = mapper;
            _typeRepo = type;
        }
        public async Task<Guid> AddPolicy(PolicyDto policy)
        {
            check(policy);
         
            var newPolicy = _Mapper.Map<PolicyDto, Policy>(policy);
            Policy policyAdded = _PolicyRepo.Add(newPolicy);
            return policyAdded.Id;
        }

        public void check(PolicyDto policy)
        {
            if (_PolicyRepo.GetAll().AsNoTracking().FirstOrDefault(p => p.Name.ToLower() == policy.Name.ToLower()) != null)
            {
                throw new DataAlreadyPresnetException("Policy scheme already exist!");
            }
        }
        public async Task<Guid> UpdatePolicy(PolicyDto policy)
        {
     
            if (_PolicyRepo.GetAll().AsNoTracking().FirstOrDefault(p=>p.Id == policy.Id) == null)
            {
                throw new InvalidGuidException("Invalid Policy!");
            }
            return _PolicyRepo.Update(_Mapper.Map<PolicyDto, Policy>(policy)).Id;
        }

        public async Task<List<PolicyDto>> GetPolicies()
        {
            return _Mapper.Map<List<Policy>, List<PolicyDto>>(_PolicyRepo.GetAll().ToList());
        }

        public async Task<PolicyDto> GetPolicy(Guid policyId)
        {
            return _Mapper.Map<Policy, PolicyDto>(_PolicyRepo.GetAll()
                .Include(p => p.PolicyAccounts)
                .FirstOrDefault(x => x.Id == policyId));
        }

        public async Task<List<PolicyDto>> GetPoliciesByTypeId(Guid PolicyTypeId)
        {
            if (_typeRepo.Get(PolicyTypeId) == null)
            {
                throw new InvalidGuidException("Policy Plan not found!");
            }
            return _Mapper.Map<List<PolicyDto>>(_PolicyRepo.GetAll().Where(p=>p.PolicyTypeId == PolicyTypeId).ToList());
        }

        public async Task<bool> DeletePolicy(Guid policyId)
        {
            var policy = _PolicyRepo.GetAll().AsNoTracking().FirstOrDefault(p=>p.Id == policyId);
            if (policy == null) {
                throw new InvalidGuidException("Invalid policy!");
            }
            policy.IsActive = false;
            _PolicyRepo.Update(policy);
            return true;
        }
    }
}

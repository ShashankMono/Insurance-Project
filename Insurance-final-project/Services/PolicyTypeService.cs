using AutoMapper;
using Insurance_final_project.Dto;
using Insurance_final_project.Exceptions;
using Insurance_final_project.Models;
using Insurance_final_project.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Insurance_final_project.Services
{
    public class PolicyTypeService:IPolicyTypeService
    {

        private readonly IRepository<PolicyType> _PolicyTypeRepo;
        private readonly IMapper _Mapper;
        public PolicyTypeService(IRepository<PolicyType> repo, IMapper mapper)
        {
            _PolicyTypeRepo = repo;
            _Mapper = mapper;
        }
        public async Task<List<PolicyTypeDto>> GetPolicyType()
        {
            return _Mapper.Map<List<PolicyType>, List<PolicyTypeDto>>(_PolicyTypeRepo.GetAll().ToList());
        }
        public async Task<Guid> AddPolicyType(PolicyTypeDto policyType)
        {
            var newPolicyType = _Mapper.Map<PolicyTypeDto, PolicyType>(policyType);
            if (_PolicyTypeRepo.GetAll().FirstOrDefault(p => p.Type.ToLower() == newPolicyType.Type.ToLower()) != null)
            {
                throw new PolicyTypeExistException("Type of policy already exist!");
            }
            PolicyType policyTypeAdded = _PolicyTypeRepo.Add(newPolicyType);
            return policyTypeAdded.Id;
        }

        public async Task<Guid> UpdatePolicyType(PolicyTypeDto policyType)
        {
            if (_PolicyTypeRepo.GetAll().AsNoTracking().FirstOrDefault(pt=>pt.Id == policyType.Id) == null)
            {
                throw new InvalidGuidException("Invalid Policy type!");
            }
            return _PolicyTypeRepo.Update(_Mapper.Map<PolicyType>(policyType)).Id;
        }

        public bool DeletePolicyType(Guid Id)
        {
            var policyType = _PolicyTypeRepo.GetAll().AsNoTracking().FirstOrDefault(p=>p.Id == Id);
            if (policyType == null) {
                throw new PolicyTypeNotFoundException("Policy type not found!");
            }
            policyType.IsActive = false;
            _PolicyTypeRepo.Update(policyType);
            return true;
        }
    }
}

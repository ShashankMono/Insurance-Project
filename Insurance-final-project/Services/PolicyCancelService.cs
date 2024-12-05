using AutoMapper;
using Insurance_final_project.Constant;
using Insurance_final_project.Dto;
using Insurance_final_project.Exceptions;
using Insurance_final_project.Models;
using Insurance_final_project.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Insurance_final_project.Services
{
    public class PolicyCancelService:IPolicyCancelService
    {
        private readonly IRepository<PolicyAccount> _policyAccountRepository;
        private readonly IMapper _Mapper;
        private readonly IRepository<PolicyCancel> _policyCancelRepository;
        public PolicyCancelService(IRepository<PolicyAccount> repo, IMapper mapper, IRepository<PolicyCancel> policyCancelRepository)
        {
            _policyAccountRepository = repo;
            _Mapper = mapper;
            _policyCancelRepository = policyCancelRepository;
        }
        public async Task<bool> CancelPolicy(Guid policyAccountId)
        {
            var policyAccount = _policyAccountRepository.GetAll().AsNoTracking().FirstOrDefault(pa=>pa.Id ==policyAccountId);
            if (policyAccount == null || policyAccount.Status == "Closed")
                throw new AlreadyCancelledPolicyException("PolcyAccount has been closed!");

            if (policyAccount.EndDate <= DateTime.UtcNow)
            {
                policyAccount.Status = "Closed";
                _policyAccountRepository.Update(policyAccount);

                var policyCancel = new PolicyCancel
                {
                    PolicyAccountId = policyAccount.Id,
                    Amount = policyAccount.TotalAmountPaid,
                    IsApproved = ApprovalType.Pending.ToString(),
                    DateAndTime = DateTime.UtcNow
                };

                _policyCancelRepository.Add(policyCancel);
                return true;
            }

            return false;
        }

        public async Task<Guid> ApprovePolicyCancelation(ApprovalDto policyCancel)
        {
            var policyCan = _policyCancelRepository.GetAll().AsNoTracking().FirstOrDefault(pc=>pc.PolicyCancelId == policyCancel.Id);
            if(policyCan == null)
            {
                throw new InvalidGuidException("Policy cancelation not found!");
            }
            if (policyCan.IsApproved == ApprovalType.Approved.ToString())
            {
                var policyAccount = _policyAccountRepository.GetAll().FirstOrDefault(pa => pa.Id == policyCan.PolicyAccountId);
                if (policyAccount != null) throw new PolicyAccountNotFountException("Policy Account not found!");
                policyAccount.Status = PolicyAccountStatus.Closed.ToString();
                _policyAccountRepository.Update(policyAccount);
            }
            policyCan.IsApproved = policyCancel.IsApproved;
            PolicyCancel updatedPolicyCancel = _policyCancelRepository.Update(policyCan);

            return updatedPolicyCancel.PolicyCancelId;
        }

        public async Task<List<PolicyCancelDto>> GetPolicyCancels()
        {
            return _Mapper.Map<List<PolicyCancel>, List<PolicyCancelDto>>(_policyCancelRepository.GetAll().ToList());
        }
    }
}

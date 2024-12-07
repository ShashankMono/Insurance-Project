using AutoMapper;
using Insurance_final_project.Constant;
using Insurance_final_project.Dto;
using Insurance_final_project.Exceptions;
using Insurance_final_project.Models;
using Insurance_final_project.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Insurance_final_project.Services
{
    public class PolicyAccountService : IPolicyAccountService
    {
        private readonly IRepository<PolicyAccount> _PolicyAccountRepo;
        private readonly IMapper _Mapper;
        private readonly IPolicyInstallmentService _PolicyInstallmentService;
        private readonly ICommissionService _commissionService;
        private readonly IMapper _mapper;
        private readonly IRepository<Agent> _agentRepo;
        private readonly IRepository<Policy> _policyRepo;
        private readonly IRepository<Customer> _customerRepo;
        public PolicyAccountService(IRepository<PolicyAccount> repo
            , IMapper mapper
            , ICommissionService commissionService
            , IPolicyInstallmentService PolicyInstallmentService
            , IRepository<Agent> agent
            , IRepository<Policy> policyRepo
            , IRepository<Customer> customerRepo)
        {
            _PolicyAccountRepo = repo;
            _Mapper = mapper;
            _PolicyInstallmentService = PolicyInstallmentService;
            _commissionService = commissionService;
            _agentRepo = agent;
            _policyRepo = policyRepo;
            _customerRepo = customerRepo;
        }

        public async Task<PolicyAccountDto> GetPolicyAccountById(Guid policyAccountId)
        {
            return _Mapper.Map<PolicyAccount, PolicyAccountDto>(_PolicyAccountRepo.GetAll()
                .Include(p => p.Policy)
                .FirstOrDefault(p => p.Id == policyAccountId)
                );
        }

        public async Task<List<PolicyAccountDto>> GetAllPolicyAccounts()
        {
            return _Mapper.Map<List<PolicyAccount>, List<PolicyAccountDto>>(_PolicyAccountRepo.GetAll().ToList());
        }

        public async Task<Guid> CreatePolicyAccount(PolicyAccountDto policyAccountDto)
        {
            var policyAccount = _Mapper.Map<PolicyAccount>(policyAccountDto);
            var customer = _customerRepo.Get(policyAccount.CustomerId);
            if (customer == null)
            {
                throw new InvalidGuidException("Customer not found!");
            }
            //else if(customer.IsApproved == ApprovalType.Pending.ToString())
            //{
            //    throw new CustomerNotApprovedException("Customer approval pending!");
            //}
            //else if (customer.IsApproved == ApprovalType.Rejected.ToString())
            //{
            //    throw new CustomerNotApprovedException("Customer approval Rejected!");
            //}
            var policy = _policyRepo.Get(policyAccountDto.PolicyId);

            if (policy == null || !policy.IsActive) {
                throw new PolicyNotFoundException("Policy not found!");
            }
            if (policyAccount.AgentId != null && _agentRepo.Get((Guid)policyAccount.AgentId) == null)
            {
                throw new InvalidGuidException("Agent not found!");
            }
            policyAccount.StartDate = DateTime.UtcNow;
            policyAccount.EndDate = DateTime.UtcNow.AddYears(policyAccountDto.PolicyTerm);//years


            _PolicyAccountRepo.Add(policyAccount);
            //AddInstallments(policyId,startDate,years, choice, totalAmount)
            //_PolicyInstallmentService.AddInstallments(
            //    policyAccount.Id,
            //    policyAccount.StartDate,
            //    policyAccountDto.PolicyTerm,
            //    policyAccountDto.InstallmentType,
            //    policyAccountDto.CoverageAmount,
            //    policyAccountDto.CustomerId
            //    );

            //if (policyAccount.AgentId != null)
            //{
            //    var commission = new CommissionDto()
            //    {
            //        PolicyAccountId = policyAccount.Id,
            //        AgentId = (Guid)policyAccount.AgentId,
            //        Date = DateTime.UtcNow,
            //        CommissionType = CommissionType.Regisration.ToString()
            //    };
            //    _commissionService.AddCommission(commission, policyAccount.TotalAmountPaid);
            //}

            return policyAccount.Id;
        }


        public async Task<List<PolicyAccountDto>> GetPolicyAccountsByAgent(Guid agentId)
        {
            var agent = _agentRepo.Get(agentId);
            if (agent == null)
                throw new Exception("Agent not found.");

            var policyAccounts = _PolicyAccountRepo.GetAll()
                .Where(pa => pa.AgentId == agentId)
                .ToList();

            return _mapper.Map<List<PolicyAccountDto>>(policyAccounts);
        }

        public async Task<List<PolicyAccountDto>> GetPoliciesByCustomer(Guid customerId)
        {
            var policies = _PolicyAccountRepo.GetAll()
                .Where(p => p.CustomerId == customerId)
                .ToList();
            return _mapper.Map<List<PolicyAccountDto>>(policies);
        }

    }
}

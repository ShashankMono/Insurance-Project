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

        public async Task<PolicyAccountResponseDto> GetPolicyAccountById(Guid policyAccountId)
        {
            return _Mapper.Map<PolicyAccount, PolicyAccountResponseDto>(_PolicyAccountRepo.GetAll()
                .Include(p => p.Policy)
                .FirstOrDefault(p => p.Id == policyAccountId)
                );
        }

        public async Task<List<PolicyAccountResponseDto>> GetAllPolicyAccounts()
        {
            return _Mapper.Map<List<PolicyAccount>, List<PolicyAccountResponseDto>>(_PolicyAccountRepo.GetAll().Include(p=>p.Policy).ToList());
        }

        public async Task<Guid> CreatePolicyAccount(PolicyAccountDto policyAccountDto)
        {
            var policyAccount = _Mapper.Map<PolicyAccount>(policyAccountDto);
            var customer = _customerRepo.Get(policyAccount.CustomerId);
            var policy = _policyRepo.Get(policyAccountDto.PolicyId);
            var age = DateTime.Today.Year - customer.DateOfBirth.Year;

            if (customer == null)
            {
                throw new InvalidGuidException("Customer not found!");
            }
            else if (customer.IsApproved == ApprovalType.Pending.ToString())
            {
                throw new CustomerNotApprovedException("Customer approval pending!");
            }
            else if (customer.IsApproved == ApprovalType.Rejected.ToString())
            {
                throw new CustomerNotApprovedException("Customer approval Rejected!");
            }else if ( age < policy.MinimumAgeCriteria && age>policy.MaximumAgeCriteria)
            {
                throw new InvalidAgeException($"Customer of this age not eligible{age}");
            }

            if (policy == null || !policy.IsActive) {
                throw new PolicyNotFoundException("Policy not found!");
            }else if (policy.MinimumInvestmentAmount<policyAccount.InvestmentAmount && policy.MaximumInvestmentAmount < policyAccount.InvestmentAmount)
            {
                throw new InvestmentAmountInvalidException("Invalid Investment Amount");
            }
            if (policyAccount.AgentId != null && _agentRepo.Get((Guid)policyAccount.AgentId) == null)
            {
                throw new InvalidGuidException("Agent not found!");
            }
            policyAccount.StartDate = DateTime.UtcNow;
            policyAccount.EndDate = DateTime.UtcNow.AddYears(policyAccountDto.PolicyTerm);
            policyAccount.CoverageAmount = policyAccount.InvestmentAmount + (policyAccount.InvestmentAmount * (policy.ProfitPercentage * 0.01));
            _PolicyAccountRepo.Add(policyAccount);

            return policyAccount.Id;
        }


        public async Task<List<PolicyAccountResponseDto>> GetPolicyAccountsByAgent(Guid agentId)
        {
            var agent = _agentRepo.Get(agentId);
            if (agent == null)
                throw new Exception("Agent not found.");

            var policyAccounts = _PolicyAccountRepo.GetAll()
                .Where(pa => pa.AgentId == agentId)
                .ToList();

            return _mapper.Map<List<PolicyAccountResponseDto>>(policyAccounts);
        }

        public async Task<List<PolicyAccountResponseDto>> GetPoliciesByCustomer(Guid customerId)
        {
            var policies = _PolicyAccountRepo.GetAll()
                .Where(p => p.CustomerId == customerId)
                .ToList();
            return _mapper.Map<List<PolicyAccountResponseDto>>(policies);
        }

    }
}

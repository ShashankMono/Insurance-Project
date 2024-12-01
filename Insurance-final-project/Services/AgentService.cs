using AutoMapper;
using Insurance_final_project.Dto;
using Insurance_final_project.Models;
using Insurance_final_project.Repositories;

namespace Insurance_final_project.Services
{
    public class AgentService : IAgentService
    {
        private readonly IRepository<Agent> _agentRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<PolicyAccount> _policyAccountRepository;
        private readonly IRepository<CommissionWithdrawal> _withdrawalRepository;
        private readonly IRepository<Policy> _policyRepository;
        private readonly IRepository<Claim> _claimRepository;
        private readonly IMapper _mapper;

        public AgentService(
            IRepository<Agent> agentRepository,
            IRepository<Customer> customerRepository,
            IRepository<PolicyAccount> policyAccountRepository,
            IRepository<CommissionWithdrawal> withdrawalRepository,
            IRepository<Policy> policyRepository,
            IRepository<Claim> claimRepository,
            IMapper mapper)
        {
            _agentRepository = agentRepository;
            _customerRepository = customerRepository;
            _policyAccountRepository = policyAccountRepository;
            _withdrawalRepository = withdrawalRepository;
            _policyRepository = policyRepository;
            _claimRepository = claimRepository;
            _mapper = mapper;
        }

        public AgentDto RegisterAgent(AgentDto agentDto)
        {
            var agent = _mapper.Map<Agent>(agentDto);
            agent = _agentRepository.Add(agent);
            return _mapper.Map<AgentDto>(agent);
        }

        public AgentDto GetAgentById(Guid agentId)
        {
            var agent = _agentRepository.Get(agentId);
            if (agent == null)
                throw new Exception("Agent not found.");
            return _mapper.Map<AgentDto>(agent);
        }
        // View Customers - only those that have bought the policy with the help of that agent
        //public ICollection<CustomerDto> GetCustomersByAgent(Guid agentId)
        //{
        //    var customers = _customerRepository.GetAll()
        //    .Where(c => c.AgentId == agentId && c.PolicyAccounts.Any())
        //    .ToList();
        //    return _mapper.Map<ICollection<CustomerDto>>(customers);
        //}

        public void RecommendPlan(Guid customerId, Guid policyId, Guid agentId, PolicyAccountDto policyAccountDto)
        {
            var policyAccount = new PolicyAccount
            {
                PolicyId = policyId,
                CustomerId = customerId,
                AgentId = agentId,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddYears(policyAccountDto.EndDate.Year - DateTime.Now.Year),
                Status = "Pending",
                InstallmentType = "Monthly",
                CoverageAmount = 0,
                TotalAmountPaid = 0,
            };

            _policyAccountRepository.Add(policyAccount);
        }

        public ICollection<CommissionWithdrawalDto> GetCommissionWithdrawals(Guid agentId)
        {
            var withdrawals = _withdrawalRepository.GetAll()
                .Where(w => w.AgentId == agentId)
                .ToList();
            return _mapper.Map<ICollection<CommissionWithdrawalDto>>(withdrawals);
        }

        public void WithdrawCommission(Guid agentId, double amount)
        {
            var agent = _agentRepository.Get(agentId);
            if (agent == null)
                throw new Exception("Agent not found.");
            if (agent.CommissionEarned < amount)
                throw new Exception("Insufficient commission balance.");

            var withdrawal = new CommissionWithdrawal
            {
                AgentId = agentId,
                Amount = amount,
                TransactionDate = DateTime.Now,
                ApprovedStatus = false,
                TransactionStatus = false,
            };

            _withdrawalRepository.Add(withdrawal);
        }

        public double ViewTotalCommission(Guid agentId)
        {
            var agent = _agentRepository.Get(agentId);
            if (agent == null)
                throw new Exception("Agent not found.");
            return agent.CommissionEarned;
        }

        //public ICollection<ClaimDto> ViewCustomerClaims(Guid agentId)
        //{
        //    var claims = _policyAccountRepository.GetAll()
        //        .Where(p => p.AgentId == agentId)
        //        .SelectMany(p => p.policyInstallments)
        //        .SelectMany(i => i.PolicyAccount.Claims)
        //        .ToList();
        //    return _mapper.Map<ICollection<ClaimDto>>(claims);
        //}
    }
}

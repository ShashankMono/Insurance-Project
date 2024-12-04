using AutoMapper;
using Insurance_final_project.Constant;
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
        private readonly IRepository<Transaction> _transactionRepository;
        private readonly IRepository<Policy> _policyRepository;
        private readonly IRepository<Claim> _claimRepository;
        private readonly IMapper _mapper;

        public AgentService(
            IRepository<Agent> agentRepository,
            IRepository<Customer> customerRepository,
            IRepository<PolicyAccount> policyAccountRepository,
            IRepository<CommissionWithdrawal> withdrawalRepository,
            IRepository<Transaction> transactionRepository,
            IRepository<Policy> policyRepository,
            IRepository<Claim> claimRepository,
            IMapper mapper)
        {
            _agentRepository = agentRepository;
            _customerRepository = customerRepository;
            _policyAccountRepository = policyAccountRepository;
            _withdrawalRepository = withdrawalRepository;
            _transactionRepository = transactionRepository;
            _policyRepository = policyRepository;
            _claimRepository = claimRepository;
            _mapper = mapper;
        }

        public AgentInputDto GetAgentById(Guid agentId)
        {
            var agent = _agentRepository.Get(agentId);
            if (agent == null)
                throw new Exception("Agent not found.");
            return _mapper.Map<AgentInputDto>(agent);
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
                ApprovedStatus = ApprovalType.Pending.ToString(),
                TransactionStatus = false,
            };

            _withdrawalRepository.Add(withdrawal);
            // Update the agent's commission balance
            agent.CommissionEarned -= amount;
            _agentRepository.Update(agent);
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Type = "Commission Withdrawal",
                Amount = amount,
                CustomerId = Guid.Empty, 
                PolicyAccountId = Guid.Empty,
                DateTime = DateTime.UtcNow,
                ReferenceNumber = Guid.NewGuid().ToString()
            };

            _transactionRepository.Add(transaction);
        }

        public double ViewTotalCommission(Guid agentId)
        {
            var agent = _agentRepository.Get(agentId);
            if (agent == null)
                throw new Exception("Agent not found.");
            return agent.CommissionEarned;
        }

        public ICollection<PolicyAccountDto> GetPolicyAccountsByAgent(Guid agentId)
        {
            var agent = _agentRepository.Get(agentId);
            if (agent == null)
                throw new Exception("Agent not found.");

            var policyAccounts = _policyAccountRepository.GetAll()
                .Where(pa => pa.AgentId == agentId)
                .ToList();

            return _mapper.Map<ICollection<PolicyAccountDto>>(policyAccounts);
        }
    }
}

using AutoMapper;
using Insurance_final_project.Dto;
using Insurance_final_project.Exceptions;
using Insurance_final_project.Models;
using Insurance_final_project.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Insurance_final_project.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Agent> _AgentRepo;
        private readonly IRepository<Employee> _EmployeeRepo;
        private readonly IRepository<Role> _RoleRepo;
        private readonly IRepository<Policy> _PolicyRepo;
        private readonly IRepository<User> _UserRepo;
        private readonly IRepository<Customer> _CustomerRepo;
        private readonly IRepository<Commission> _CommissionRepo;
        private readonly IRepository<CommissionWithdrawal> _CommissionWithdrawalRepo;
        private readonly IRepository<Claim> _ClaimRepo;
        private readonly IRepository<PolicyAccount> _PolicyAccountRepo;
        private readonly IRepository<Document> _DocumentRepo;
        private readonly IRepository<PolicyCancel> _PolicyCancelRepo;
        private readonly IRepository<Transaction> _TransactionRepo;
        private readonly IMapper _Mapper;
        public EmployeeService(
        IRepository<Agent> agentRepo,
        IRepository<Employee> employeeRepo,
        IRepository<Policy> policyRepo,
        IRepository<Role> RoleRepo,
        IRepository<Document> document,
        IRepository<User> userRepo,
        IRepository<Customer> customerRepo,
        IRepository<Commission> commissionRepo,
        IRepository<CommissionWithdrawal> commissionWithdrawalRepo,
        IRepository<Claim> claimRepo,
        IRepository<PolicyAccount> policyAccountRepo,
        IRepository<PolicyCancel> policyCancelRepo,
        IRepository<Transaction> transactionRepo,
        IMapper mapper)
        {
            _AgentRepo = agentRepo;
            _EmployeeRepo = employeeRepo;
            _PolicyRepo = policyRepo;
            _DocumentRepo = document;
            _CustomerRepo = customerRepo;
            _CommissionRepo = commissionRepo;
            _CommissionWithdrawalRepo = commissionWithdrawalRepo;
            _UserRepo = userRepo;
            _ClaimRepo = claimRepo;
            _PolicyAccountRepo = policyAccountRepo;
            _RoleRepo = RoleRepo;
            _PolicyCancelRepo = policyCancelRepo;
            _TransactionRepo = transactionRepo;
            _Mapper = mapper;
        }
        public async Task<UserDto> AddAgent(AgentInputDto newAgent)
        {
            var newUsername = Guid.NewGuid().ToString();
            Random random = new Random();
            var password = random.Next(1001, 10000).ToString();
            UserDto user = new UserDto()
            {
                Username = newUsername,
                Password = password,
                RoleId = _RoleRepo.GetAll().FirstOrDefault(r => r.RoleName == "Agent").RoleId,
            };
            User userAgent = _UserRepo.Add(_Mapper.Map<UserDto, User>(user));
            user.UserId = userAgent.UserId;
            Agent agent = _Mapper.Map<AgentInputDto, Agent>(newAgent);
            agent.UserId = user.UserId;
            Agent agentAdded = _AgentRepo.Add(agent);
            return user;

        }

        public async Task<Guid> ChangeApproveStatus(DocumentDto document)
        {
            return _DocumentRepo.Update(_Mapper.Map<DocumentDto, Document>(document)).DocumentId;
        }

        public async Task<AgentInputDto> GetAgentReport(AgentInputDto agent)
        {
            return _Mapper.Map<Agent, AgentInputDto>(_AgentRepo.GetAll()
            .Include(a => a.PolicyAccounts)
            .Include(a => a.Commissions)
            .Include(a => a.CommissionWithdrawal)
            .FirstOrDefault(a => a.AgentId == agent.AgentId));
        }

        public async Task<List<AgentInputDto>> GetAllAgents()
        {
            return _Mapper.Map<List<Agent>, List<AgentInputDto>>(_AgentRepo.GetAll().ToList());
        }

        public async Task<List<PolicyAccountDto>> GetAllPolicyAccounts()
        {
            return _Mapper.Map<List<PolicyAccount>, List<PolicyAccountDto>>(_PolicyAccountRepo.GetAll().ToList());
        }

        public async Task<List<ClaimDto>> GetClaimAccounts()
        {
            return _Mapper.Map<List<Claim>, List<ClaimDto>>(_ClaimRepo.GetAll().Include(c => c.Document).ToList());
        }

        public async Task<List<CommissionDto>> GetCommissions()
        {
            return _Mapper.Map<List<Commission>, List<CommissionDto>>(_CommissionRepo.GetAll().ToList());
        }

        public async Task<List<CommissionWithdrawalDto>> GetCommissionsWithdrawal()
        {
            return _Mapper.Map<List<CommissionWithdrawal>, List<CommissionWithdrawalDto>>(_CommissionWithdrawalRepo.GetAll().ToList());
        }

        public async Task<List<CustomerDto>> GetCustomerAccounts()
        {
            return _Mapper.Map<List<Customer>, List<CustomerDto>>(_CustomerRepo.GetAll()
                .Include(c => c.Transactions)
                .Include(c => c.PolicyAccounts)
                .Include(c => c.Queries)
                .ToList());
        }

        public async Task<List<PolicyDto>> GetPolicies()
        {
            return _Mapper.Map<List<Policy>, List<PolicyDto>>(_PolicyRepo.GetAll().ToList());
        }

        public async Task<PolicyAccountDto> GetPolicyAccount(PolicyAccountDto policyAccount)
        {
            return _Mapper.Map<PolicyAccount, PolicyAccountDto>(_PolicyAccountRepo.GetAll()
                .Include(p => p.Policy)
                .Include(p => p.policyInstallments)
                .ThenInclude(installment => installment.Transaction)
                .FirstOrDefault(p => p.Id == policyAccount.Id)
                );
        }

        public async Task<List<PolicyCancelDto>> GetPolicyCancels()
        {
            return _Mapper.Map<List<PolicyCancel>, List<PolicyCancelDto>>(_PolicyCancelRepo.GetAll().ToList());
        }

        public async Task<PolicyDto> GetPolicy(Guid policyId)
        {
            return _Mapper.Map<Policy, PolicyDto>(_PolicyRepo.GetAll()
                .Include(p => p.PolicyAccounts)
                .FirstOrDefault(x => x.Id == policyId));
        }

        public async Task<List<TransactionDto>> GetTransactions()
        {
            return _Mapper.Map<List<Transaction>, List<TransactionDto>>(_TransactionRepo.GetAll().ToList());
        }

        public async Task<Guid> UpdateEmployeeProfile(EmployeeDto employee)
        {
            if(_EmployeeRepo.Get(_Mapper.Map<EmployeeDto, Employee>(employee).EmployeeId) == null)
            {
                throw new InvalidEmployeeException("Employee not found!");
            }
            return _EmployeeRepo.Update(_Mapper.Map<EmployeeDto, Employee>(employee)).EmployeeId;
        }
    }
}

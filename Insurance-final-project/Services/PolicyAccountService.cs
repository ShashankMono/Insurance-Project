using AutoMapper;
using Insurance_final_project.Constant;
using Insurance_final_project.Dto;
using Insurance_final_project.Exceptions;
using Insurance_final_project.Models;
using Insurance_final_project.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Security.Permissions;

namespace Insurance_final_project.Services
{
    public class PolicyAccountService : IPolicyAccountService
    {
        private readonly IRepository<PolicyAccount> _PolicyAccountRepo;
        private readonly IMapper _Mapper;
        private readonly IPolicyInstallmentService _PolicyInstallmentService;
        private readonly ICommissionService _commissionService;
        private readonly IRepository<Agent> _agentRepo;
        private readonly IRepository<Policy> _policyRepo;
        private readonly IRepository<Customer> _customerRepo;
        private readonly IEmailService _emailService;
        public PolicyAccountService(IRepository<PolicyAccount> repo
            , IMapper mapper
            , ICommissionService commissionService
            , IPolicyInstallmentService PolicyInstallmentService
            , IRepository<Agent> agent
            , IRepository<Policy> policyRepo
            , IRepository<Customer> customerRepo
            , IEmailService emailService)
        {
            _PolicyAccountRepo = repo;
            _Mapper = mapper;
            _PolicyInstallmentService = PolicyInstallmentService;
            _commissionService = commissionService;
            _agentRepo = agent;
            _policyRepo = policyRepo;
            _customerRepo = customerRepo;
            _emailService = emailService;
        }



        public async Task<PolicyAccountResponseDto> GetPolicyAccountById(Guid policyAccountId)
        {
            return _Mapper.Map<PolicyAccount, PolicyAccountResponseDto>(_PolicyAccountRepo.GetAll()
                .Include(p => p.Policy)
                .Include(pa => pa.Customer)
                .FirstOrDefault(p => p.Id == policyAccountId)
                );
        }

        public async Task<List<PolicyAccountResponseDto>> GetAllPolicyAccounts(string searchQuery)
        {
            var query = _PolicyAccountRepo.GetAll()
                 .Include(pa => pa.Policy)
                 .Include(pa => pa.Customer)
                 .Include(pa => pa.Agent)
                 .OrderByDescending(p => p.Id)
                 .AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(pa => pa.Policy.Name.ToLower() == searchQuery.ToLower() ||
                    (pa.Customer.FirstName + " " + pa.Customer.LastName).ToLower() == searchQuery.ToLower() ||
                    pa.Status.ToLower() == searchQuery.ToLower() ||
                    pa.IsApproved.ToLower() == searchQuery.ToLower()
                     );
            }

            return _Mapper.Map<List<PolicyAccount>, List<PolicyAccountResponseDto>>(query.ToList());
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
            }else if (policy.MinimumInvestmentAmount>policyAccount.InvestmentAmount || policy.MaximumInvestmentAmount < policyAccount.InvestmentAmount)
            {
                throw new InvestmentAmountInvalidException("Invalid Investment Amount");
            }
            else if (policy.MinimumPolicyTerm > policyAccount.PolicyTerm || policy.MaximumPolicyTerm < policyAccount.PolicyTerm)
            {
                throw new PolicyTermInvalidException("Invalid policy Term");
            }
            if (policyAccount.AgentId != null && _agentRepo.Get((Guid)policyAccount.AgentId) == null)
            {
                throw new InvalidGuidException("Agent not found!");
            }
            policyAccount.StartDate = DateTime.UtcNow;
            policyAccount.EndDate = DateTime.UtcNow.AddYears(policyAccountDto.PolicyTerm);
            policyAccount.CoverageAmount = policyAccount.InvestmentAmount + (policyAccount.InvestmentAmount * (policy.ProfitPercentage * 0.01));
            policyAccount.Status = PolicyAccountStatus.Open.ToString();
            _PolicyAccountRepo.Add(policyAccount);

            return policyAccount.Id;
        }

        public async Task<bool> ApproveAccount(ApprovalDto approval)
        {
            var existingAccount = _PolicyAccountRepo.GetAll().AsNoTracking().Include(pa=>pa.Policy).FirstOrDefault(pa => pa.Id == approval.Id);
            if (existingAccount == null)
            {
                throw new PolicyAccountNotFountException("Account not found!");
            }
            
            existingAccount.IsApproved = approval.IsApproved.ToLower() == "Approved".ToLower() || approval.IsApproved == "Approve".ToLower()
                                                            ? ApprovalType.Approved.ToString() : ApprovalType.Rejected.ToString();
            if (approval.IsApproved.ToLower() == VerificationType.Rejected.ToString().ToLower())
            {
                existingAccount.Status = PolicyAccountStatus.Closed.ToString();
                _emailService.RejectionMail(existingAccount.CustomerId, approval.Reason,
                    $"{existingAccount.Policy.Name} Kyc rejected");
            }
            _PolicyAccountRepo.Update(existingAccount);
            return true;
        }


        public async Task<List<PolicyAccountResponseDto>> GetPolicyAccountsByAgent(Guid agentId)
        {
            var agent = _agentRepo.Get(agentId);
            if (agent == null)
                throw new Exception("Agent not found.");

            var policyAccounts = _PolicyAccountRepo.GetAll()
                .Where(pa => pa.AgentId == agentId)
                .Include(pa=>pa.Policy)
                .Include(pa=>pa.Customer)
                .Include(pa=>pa.Agent)
                .ToList();

            return _Mapper.Map<List<PolicyAccountResponseDto>>(policyAccounts);
        }


        public async Task<List<PolicyAccountResponseDto>> GetPoliciesByCustomer(Guid customerId)
        {
            var policies = _PolicyAccountRepo.GetAll()
                .Where(p => p.CustomerId == customerId)
                .Include(pa => pa.Policy)
                .Include(pa => pa.Customer)
                .Include(pa => pa.Agent)
                .ToList();
            return _Mapper.Map<List<PolicyAccountResponseDto>>(policies);
        }

    }
}

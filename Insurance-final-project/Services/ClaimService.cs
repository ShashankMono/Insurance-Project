using AutoMapper;
using Insurance_final_project.Constant;
using Insurance_final_project.Dto;
using Insurance_final_project.Exceptions;
using Insurance_final_project.Models;
using Insurance_final_project.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Insurance_final_project.Services
{
    public class ClaimService:IClaimService
    {
        private readonly IRepository<Claim> _ClaimRepo;
        private readonly IMapper _Mapper;
        private readonly IRepository<PolicyAccount> _policyAccountRepository;
        private readonly ITransactionService _transactionService;
        private readonly IRepository<Customer> _customerRepo;
        private readonly IEmailService _emailService;
        public ClaimService(IRepository<Claim> repo ,
            ITransactionService transaction,
            IMapper mapper,
            IRepository<PolicyAccount>policyAccount,
            IRepository<Customer> customerRepo,
            IEmailService emailService
            )
        {
            _ClaimRepo = repo;
            _Mapper = mapper;
            _policyAccountRepository = policyAccount;
            _transactionService = transaction;
            _customerRepo = customerRepo;
            _emailService = emailService;
        }

        public async Task<List<ClaimDto>> GetClaimAccounts()
        {
            return _Mapper.Map<List<Claim>, List<ClaimDto>>(_ClaimRepo.GetAll()
                .Include(c=>c.PolicyAccount)
                .ThenInclude(p=>p.Policy)
                .ToList());
        }
        public async Task<Guid> ClaimApproval(ApprovalDto claim)
        {
            var claimAccount = _ClaimRepo.GetAll().AsNoTracking()
                .Include(c=>c.PolicyAccount)
                .ThenInclude(p=>p.Customer)
                .Include(c=>c.PolicyAccount)
                .ThenInclude(p=>p.Policy)
                .FirstOrDefault(c => c.ClaimId == claim.Id);

            if (claimAccount == null) {
                throw new InvalidClaimRequestException("Claim not found");
            }

            claimAccount.ApprovedStatus = claim.IsApproved;
            claimAccount.AcknowledgementDate = DateTime.UtcNow;
            var mail = claimAccount.PolicyAccount.Customer.EmailId;
            var subject =$"Claim request status changed" ;
            var message = $"You claim request {claimAccount.PolicyAccount.Policy.Name} {claim.IsApproved}! ";
            _emailService.ApprovalOrVrifiedMail(mail,subject,message);
            return _ClaimRepo.Update(claimAccount).ClaimId;
        }
        public async Task<Guid> AddClaimPolicy( ClaimDto claimDto)
        {
            var policyAccount = _policyAccountRepository.GetAll().AsNoTracking().Include(pa=>pa.PolicyInstallments ).FirstOrDefault(pa=>pa.Id == claimDto.PolicyAccountId);

            if (policyAccount == null || 
                policyAccount.EndDate >= DateTime.UtcNow || 
                policyAccount.PolicyInstallments.ToList().Find(i => i.InstallmentDueDate == policyAccount.EndDate).IsPaid != true)
                throw new InvalidClaimRequestException("Invalid claim request!");

            var claim = _Mapper.Map<Claim>(claimDto);
            claim.PolicyAccountId = claimDto.PolicyAccountId;
            claim.DateAndTime = DateTime.UtcNow;
            claim.ApprovedStatus = ApprovalType.Pending.ToString();
            
            return _ClaimRepo.Add(claim).ClaimId;
        }

        public async Task<List<ClaimDto>> GetClaimByCustomerId(Guid customerId)
        {
            var customer = _customerRepo.Get(customerId);
            if(customer == null)
            {
                throw new CustomerNotFoundException("Customer not found!");
            }
            var claims = _ClaimRepo.GetAll().AsNoTracking()
                .Include(c => c.PolicyAccount)
                .ThenInclude(pa=>pa.Policy)
                .Where(c => c.PolicyAccount.CustomerId == customerId)
                .ToList();

            return _Mapper.Map<List<ClaimDto>>(claims);
        }

        public async Task<Guid> CLaimWithdrawal(ClaimDto claimDto)
        {
            var claim = _ClaimRepo.GetAll().AsNoTracking().FirstOrDefault(c=>c.ClaimId==claimDto.ClaimId);
            var policyAccount = _policyAccountRepository.GetAll().AsNoTracking().Include(pa=>pa.PolicyInstallments).FirstOrDefault(pa=>pa.Id == claimDto.PolicyAccountId);
            if(claim == null)
            {
                throw new InvalidClaimIdException("Invalid Claim Id!");
            }
            if(claim.ApprovedStatus != ApprovalType.Approved.ToString())
            {
                throw new ClaimNotApprovedException("Claim not approved!");
            }
            if(policyAccount == null)
            {
                throw new PolicyAccountNotFountException("Account not found!");
            }
            if (policyAccount == null || policyAccount.EndDate > DateTime.UtcNow || policyAccount.PolicyInstallments.ToList().Find(i => i.InstallmentDueDate == policyAccount.EndDate).IsPaid != true)
                throw new InvalidClaimRequestException("Invalid claim request!");

            var transaction = new TransactionDto()
            {
                Amount = policyAccount.CoverageAmount,
                PolicyAccountId = claimDto.PolicyAccountId,
                CustomerId = _policyAccountRepository.Get(claimDto.PolicyAccountId).CustomerId,
                DateTime = DateTime.UtcNow,
                Type = TransactionType.Withdraw.ToString(),
            };

            _emailService.ClaimWithdrawalMail(claimDto.PolicyAccountId);

            return await _transactionService.AddTransaction(transaction);
        }
    }
}

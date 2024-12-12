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
        public ClaimService(IRepository<Claim> repo ,ITransactionService transaction, IMapper mapper,IRepository<PolicyAccount>policyAccount)
        {
            _ClaimRepo = repo;
            _Mapper = mapper;
            _policyAccountRepository = policyAccount;
            _transactionService = transaction;
        }

        public async Task<List<ClaimDto>> GetClaimAccounts()
        {
            return _Mapper.Map<List<Claim>, List<ClaimDto>>(_ClaimRepo.GetAll().Include(c => c.Document).ToList());
        }
        public async Task<Guid> ClaimApproval(ApprovalDto claim)
        {
            var claimAccount = _ClaimRepo.GetAll().AsNoTracking().FirstOrDefault(c => c.ClaimId == claim.Id);
            if (claimAccount == null) {
                throw new InvalidClaimRequestException("Claim not found");
            }
            claimAccount.ApprovedStatus = claim.IsApproved;
            claimAccount.AcknowledgementDate = DateTime.UtcNow;
            return _ClaimRepo.Update(claimAccount).ClaimId;
        }
        public async Task<Guid> AddClaimPolicy(Guid policyAccountId, ClaimDto claimDto)
        {
            var policyAccount = _policyAccountRepository.Get(policyAccountId);
           
            if (policyAccount == null || policyAccount.EndDate > DateTime.UtcNow)
                throw new InvalidClaimRequestException("Invalid claim request!");

            var claim = _Mapper.Map<Claim>(claimDto);
            claim.PolicyAccountId = policyAccountId;
            claim.DateAndTime = DateTime.UtcNow;
            claim.ApprovedStatus = ApprovalType.Pending.ToString();
            
            return _ClaimRepo.Add(claim).ClaimId;
        }

        public async Task<Guid> CLaimWithdrawal(ClaimDto claimDto)
        {
            var claim = _ClaimRepo.GetAll().AsNoTracking().FirstOrDefault(c=>c.ClaimId==claimDto.ClaimId);
            if(claim == null)
            {
                throw new InvalidClaimIdException("Invalid Claim Id!");
            }
            if(claim.ApprovedStatus != ApprovalType.Approved.ToString())
            {
                throw new ClaimNotApprovedException("Claim not approved!");
            }

            var transaction = new TransactionDto()
            {
                Amount = claimDto.AmountToBeClaimed,
                PolicyAccountId = claimDto.PolicyAccountId,
                CustomerId = _policyAccountRepository.Get(claimDto.PolicyAccountId).CustomerId,
                DateTime = DateTime.UtcNow,
                Type = TransactionType.Withdraw.ToString(),
            };

            return await _transactionService.AddTransaction(transaction);
        }
    }
}

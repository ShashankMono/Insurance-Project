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
        private readonly IRepository<Customer> _cutomerRepo;
        private readonly IEmailService _emailService;
        public PolicyCancelService(IRepository<PolicyAccount> repo,
            IRepository<Customer> cutomerRepo,
            IMapper mapper,
            IRepository<PolicyCancel> policyCancelRepository,
            IEmailService emailService)
        {
            _policyAccountRepository = repo;
            _Mapper = mapper;
            _policyCancelRepository = policyCancelRepository;
            _cutomerRepo = cutomerRepo;
            _emailService = emailService;
        }
        public async Task<bool> CancelPolicy(Guid policyAccountId)
        {
            var policyAccount = _policyAccountRepository.GetAll().AsNoTracking().FirstOrDefault(pa=>pa.Id ==policyAccountId);
            if (policyAccount == null || policyAccount.Status == "Closed")
                throw new AlreadyCancelledPolicyException("PolcyAccount has been closed!");
            if(_policyCancelRepository.GetAll().AsNoTracking().FirstOrDefault(pc=>pc.PolicyAccountId == policyAccountId) != null)
            {
                throw new AlreadyCancelledPolicyException("Already request send!");
            }

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

        public async Task<Guid> ApprovePolicyCancelation(ApprovalDto policyCancel)
        {
            var policyCan = _policyCancelRepository.GetAll()
                .AsNoTracking()
                .Include(pc=>pc.PolicyAccount).ThenInclude(pa=>pa.Policy)
                .Include(pc=>pc.PolicyAccount).ThenInclude(pc=>pc.Customer)
                .FirstOrDefault(pc=>pc.PolicyCancelId == policyCancel.Id);
            
            var message = policyCancel.Reason;
            if (policyCan == null)
            {
                throw new InvalidGuidException("Policy cancelation not found!");
            }
            if (policyCan.IsApproved == ApprovalType.Approved.ToString())
            {
                var policyAccount = _policyAccountRepository.GetAll().FirstOrDefault(pa => pa.Id == policyCan.PolicyAccountId);
                if (policyAccount != null) throw new PolicyAccountNotFountException("Policy Account not found!");
                policyAccount.Status = PolicyAccountStatus.Closed.ToString();
                _policyAccountRepository.Update(policyAccount);
                message = $"Your policy cancel status is for policy scheme name ${policyCan.PolicyAccount.Policy.Name} is ${policyCancel.IsApproved}";
            }
            policyCan.IsApproved = policyCancel.IsApproved;
            var mail = policyCan.PolicyAccount.Customer.EmailId;
            var subject = $"Status Changes on you policy scheme cancel on ${policyCan.PolicyAccount.Policy.Name} is ${policyCancel.IsApproved}";
            
            _emailService.ApprovalOrVrifiedMail(mail, subject, message);
            PolicyCancel updatedPolicyCancel = _policyCancelRepository.Update(policyCan);

            return updatedPolicyCancel.PolicyCancelId;
        }

        public async Task<List<PolicyCancelReponseDto>> GetPolicyCancels(Guid customerId, string? searchQuery)
        {
            if(_cutomerRepo.Get(customerId) == null)
            {
                throw new CustomerNotFoundException("Customer not found");
            }
            var query = _policyCancelRepository.GetAll()
                .Where(pc => pc.PolicyAccount.CustomerId == customerId)
                .Include(pc => pc.PolicyAccount)
                .ThenInclude(pa => pa.Policy)
                .OrderByDescending(pc => pc.PolicyCancelId)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                 searchQuery = searchQuery.ToLower();
                query = query.Where(pc => pc.PolicyAccount.Policy.Name.ToLower() == searchQuery ||
                    pc.IsApproved.ToLower() == searchQuery
                );
            }

            return _Mapper.Map<List<PolicyCancel>, List<PolicyCancelReponseDto>>(query.ToList());
        }


        public async Task<List<PolicyCancelReponseDto>> GetAllPolicyCancels( string? searchQuery)
        {
            var query = _policyCancelRepository.GetAll()
                .Include(pc => pc.PolicyAccount)
                .ThenInclude(pa => pa.Policy)
                .Include(pc=>pc.PolicyAccount).ThenInclude(pa=>pa.Customer)
                .OrderByDescending(pc => pc.PolicyCancelId)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.ToLower();
                query = query.Where(pc => pc.PolicyAccount.Policy.Name.ToLower() == searchQuery ||
                    pc.IsApproved.ToLower() == searchQuery ||
                    (pc.PolicyAccount.Customer.FirstName+ " " +pc.PolicyAccount.Customer.LastName).ToLower() == searchQuery
                );
            }

            return _Mapper.Map<List<PolicyCancel>, List<PolicyCancelReponseDto>>(query.ToList());
        }


    }
}

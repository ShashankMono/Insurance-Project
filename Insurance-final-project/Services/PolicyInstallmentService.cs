using AutoMapper;
using Insurance_final_project.Constant;
using Insurance_final_project.Dto;
using Insurance_final_project.Exceptions;
using Insurance_final_project.Models;
using Insurance_final_project.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Insurance_final_project.Services
{
    public class PolicyInstallmentService:IPolicyInstallmentService
    {
        private readonly IRepository<PolicyInstallment> _installmentRepository;
        private readonly IRepository<Transaction> _transactionRepository;
        private readonly IMapper _Mapper;
        private readonly IRepository<PolicyAccount> _policyAccountRepo;
        private readonly ICommissionService _CommissionService;

        public PolicyInstallmentService(IRepository<PolicyInstallment> installmentRepository
            , IRepository<Transaction> transactionRepository
            , IMapper mappere
            , ICommissionService commissionService)
        {
            _installmentRepository = installmentRepository;
            _transactionRepository = transactionRepository;
            _Mapper = mappere;
            _CommissionService = commissionService;
        }
        public void AddInstallments(PolicyInstallmentDto installmentData)
        {
            int installmentsPerYear = 0;

            switch (installmentData.InstallmentType)
            {
                case "Quarterly":
                    installmentsPerYear = 4;
                    break;
                case "Monthly":
                    installmentsPerYear = 12;
                    break;
                case "Half-Yearly":
                    installmentsPerYear = 2;
                    break;
                case "Yearly":
                    installmentsPerYear = 1;
                    break;
                default:
                    throw new ArgumentException("Invalid choice");
            }

            int totalInstallments = installmentsPerYear * installmentData.PolicyTerm;
            int intervalInMonths = 12 / installmentsPerYear;

            double installmentAmount = installmentData.Amount / totalInstallments;

            for (int i = 0; i < totalInstallments; i++)
            {
                DateTime installmentDueDate = installmentData.StartDate.AddMonths(i * intervalInMonths);
                bool isFirstInstallment = (i == 0);//check for first entry during creation of accountPolicy
                var installment = new PolicyInstallment
                {
                    Id = Guid.NewGuid(),
                    PolicyAccountId = installmentData.PolicyAccountId,
                    InstallmentDueDate = installmentDueDate,
                    IsPaid = false,
                    Amount = installmentAmount,
                    //InstallmentPaidDate = (DateTime)(isFirstInstallment ? installmentData.StartDate : (DateTime?)null)
                };

                _installmentRepository.Add(installment);
                //if (isFirstInstallment)
                //{
                //    var transaction = new Transaction
                //    {
                //        Type = TransactionType.Deposit.ToString(),
                //        Amount = installmentAmount,
                //        CustomerId = installmentData.CustomerId,
                //        PolicyAccountId = installmentData.PolicyAccountId,
                //        PolicyInstallmentId = installment.Id,
                //        DateTime = DateTime.UtcNow,
                //        ReferenceNumber = ""
                //    };
                //    _transactionRepository.Add(transaction);
                //}
            }
        }

        public async Task<bool> PayInstallment(Guid installmentId, Guid customerId)
        {
            var installment = _installmentRepository.GetAll().AsNoTracking().FirstOrDefault(i=>i.Id==installmentId);

            if (installment == null || installment.IsPaid)
            {
                throw new InValidRequestException("Invalid request!");
            }

            installment.IsPaid = true;
            installment.InstallmentPaidDate = DateTime.UtcNow;

            _installmentRepository.Update(installment);

            //adding transaction
            var transaction = new Transaction
            {
                Type = TransactionType.Deposit.ToString(),
                Amount = installment.Amount,
                CustomerId = customerId,
                PolicyAccountId = installment.PolicyAccountId,
                PolicyInstallmentId = installment.Id,
                DateTime = DateTime.UtcNow,
                ReferenceNumber = ""
            };

            _transactionRepository.Add(transaction);

            //Adding commission
            var policyAccount = _policyAccountRepo.Get(installment.PolicyAccountId);
            if(policyAccount.AgentId != null)
            {
                var commission = new CommissionDto()
                {
                    PolicyAccountId = installment.PolicyAccountId,
                    AgentId = (Guid)policyAccount.AgentId,
                    CommissionType = CommissionType.Installment.ToString(),
                    Date = DateTime.UtcNow,
                };
                _CommissionService.AddCommission(commission,installment.Amount);
            }

            return true;
        }

        public async Task<List<PolicyInstallmentDto>> GetInstallmentsByPolicyAccountId(Guid PolicyAccountId)
        {
            return _Mapper.Map<List<PolicyInstallmentDto>>(_installmentRepository.GetAll().Where(i=>i.PolicyAccountId == PolicyAccountId).ToList());
        }

    }
}

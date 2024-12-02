using Insurance_final_project.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Insurance_final_project.Dto;
using Insurance_final_project.Repositories;
using Microsoft.EntityFrameworkCore;
using Insurance_final_project.Data;

namespace Insurance_final_project.Services
{
    public class CustomerService:ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<PolicyAccount> _policyAccountRepository;
        private readonly IRepository<PolicyCancel> _policyCancelRepository;
        private readonly IRepository<Policy> _policyRepository;
        private readonly IRepository<PolicyInstallment> _installmentRepository;
        private readonly IRepository<Claim> _claimRepository;
        private readonly IRepository<Query> _queryRepository;
        private readonly IRepository<Transaction> _transactionRepository;
        private readonly IMapper _mapper;

        public CustomerService(
            IRepository<Customer> customerRepository,
            IRepository<User> userRepository,
            IRepository<Role> roleRepository,
            IRepository<PolicyAccount> policyAccountRepository,
            IRepository<PolicyCancel> policyCancelRepository,
            IRepository<Policy> policyRepository,
            IRepository<PolicyInstallment> installmentRepository,
            IRepository<Claim> claimRepository,
            IRepository<Query> queryRepository,
            IRepository<Transaction> transactionRepository,
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _policyAccountRepository = policyAccountRepository;
            _policyCancelRepository = policyCancelRepository;
            _policyRepository = policyRepository;
            _installmentRepository = installmentRepository;
            _claimRepository = claimRepository;
            _queryRepository = queryRepository;
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public Guid CreatePolicyAccount(PolicyAccountDto policyAccountDto)
        {
            var policyAccount = _mapper.Map<PolicyAccount>(policyAccountDto);
            
            policyAccount.StartDate = DateTime.UtcNow;
            policyAccount.EndDate = DateTime.UtcNow.AddYears(policyAccountDto.PolicyTerm);//years


            _policyAccountRepository.Add(policyAccount);
            //AddInstallments(policyId,startDate,years, choice, totalAmount)
            AddInstallments(
                policyAccount.Id,
                policyAccount.StartDate,
                policyAccountDto.PolicyTerm,
                policyAccountDto.InstallmentType,
                policyAccountDto.CoverageAmount,
                policyAccountDto.CustomerId
    );


            return policyAccount.Id;
        }
        public void AddInstallments(Guid policyAccountId, DateTime startDate, int years, string choice, double totalAmount, Guid customerId)
        {
            int installmentsPerYear = 0;

            switch (choice)
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

            int totalInstallments = installmentsPerYear * years; 
            int intervalInMonths = 12 / installmentsPerYear;

            double installmentAmount = totalAmount / totalInstallments;
            
            for (int i = 0; i < totalInstallments; i++)
            {
                DateTime installmentDueDate = startDate.AddMonths(i * intervalInMonths);
                bool isFirstInstallment = (i == 0);//check for first entry during creation of accountPolicy
                var installment = new PolicyInstallment
                {
                    Id = Guid.NewGuid(),
                    PolicyAccountId = policyAccountId,
                    InstallmentDueDate = installmentDueDate,
                    IsPaid = isFirstInstallment,
                    Amount = installmentAmount,
                    InstallmentPaidDate = (DateTime)(isFirstInstallment ? startDate : (DateTime?)null)
                };

                _installmentRepository.Add(installment);
                if (isFirstInstallment)
                {
                    var transaction = new Transaction
                    {
                        Id = Guid.NewGuid(),
                        Type = "Installment Payment",
                        Amount = installmentAmount,
                        CustomerId = customerId,
                        PolicyAccountId = policyAccountId,
                        PolicyInstallmentId = installment.Id,
                        DateTime = DateTime.UtcNow,
                        ReferenceNumber = Guid.NewGuid().ToString()
                    };
                    _transactionRepository.Add(transaction);
                }
            }

        }
        public bool PayInstallment(Guid installmentId, Guid customerId)
        {
            var installment = _installmentRepository.Get(installmentId);

            if (installment == null || installment.IsPaid)
            {
                return false;
            }

            installment.IsPaid = true;
            installment.InstallmentPaidDate = DateTime.UtcNow;

            _installmentRepository.Update(installment);

            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Type = "Installment Payment",
                Amount = installment.Amount,
                CustomerId = customerId,
                PolicyAccountId = installment.PolicyAccountId,
                PolicyInstallmentId = installment.Id,
                DateTime = DateTime.UtcNow,
                ReferenceNumber = Guid.NewGuid().ToString()
            };

            _transactionRepository.Add(transaction);
            return true;
        }
        public bool CancelPolicy(Guid policyAccountId)
        {
            var policyAccount = _policyAccountRepository.Get(policyAccountId);
            if (policyAccount == null || policyAccount.Status == "Closed")
                return false;

            if (policyAccount.EndDate <= DateTime.UtcNow)
            {
                policyAccount.Status = "Closed";
                _policyAccountRepository.Update(policyAccount);

                var policyCancel = new PolicyCancel
                {
                    PolicyAccountId = policyAccount.Id,
                    Amount = policyAccount.TotalAmountPaid,
                    IsApproved = false,
                    DateAndTime = DateTime.UtcNow
                };

                _policyCancelRepository.Add(policyCancel);
                return true;
            }

            return false;
        }

        public bool ClaimPolicy(Guid policyAccountId, ClaimDto claimDto)
        {
            var policyAccount = _policyAccountRepository.Get(policyAccountId);
            if (policyAccount == null || policyAccount.EndDate > DateTime.UtcNow)
                return false;

            var claim = _mapper.Map<Claim>(claimDto);
            claim.PolicyAccountId = policyAccountId;
            claim.DateAndTime = DateTime.UtcNow;
            claim.ApprovedStatus = false;

            _claimRepository.Add(claim);
            return true;
        }

        public void SubmitQuery(QueryDto queryDto)
        {
            var query = _mapper.Map<Query>(queryDto);
            _queryRepository.Add(query);
        }

        public CustomerDto GetCustomerById(Guid customerId)
        {
            var customer = _customerRepository.GetAll()
                .Include(c => c.PolicyAccounts)
                .Include(c => c.City)
                .Include(c => c.State)
                .FirstOrDefault(c => c.CustomerId == customerId);

            if (customer == null)
                throw new Exception("Customer not found");

            return _mapper.Map<CustomerDto>(customer);
        }

        public bool UpdateProfile(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            var existingCustomer = _customerRepository.Get(customer.CustomerId);
            if (existingCustomer != null)
            {
                
                _customerRepository.Update(customer);
                return true;
            }

            return false;
        }
        
        public CustomerDto RegisterCustomer(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            customer = _customerRepository.Add(customer);
            return _mapper.Map<CustomerDto>(customer);
        }
        
        public ICollection<PolicyAccountDto> GetPoliciesByCustomer(Guid customerId)
        {
            var policies = _policyAccountRepository.GetAll()
                .Where(p => p.CustomerId == customerId)
                .ToList();
            return _mapper.Map<ICollection<PolicyAccountDto>>(policies);
        }
    }
}

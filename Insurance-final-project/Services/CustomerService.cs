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
                policyAccountDto.CoverageAmount
    );


            return policyAccount.Id;//check
        }
        public void AddInstallments(Guid policyId, DateTime startDate, int years, string choice, double totalAmount)
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

            int totalInstallments = installmentsPerYear * years;//firstentry outside loop with obj, transaction entry added by calling Get
            int intervalInMonths = 12 / installmentsPerYear;

            double installmentAmount = totalAmount / totalInstallments;
            var firstInstallment = new PolicyInstallment
            {
                Id = Guid.NewGuid(),
                InstallmentPaidDate = startDate,
                Amount = installmentAmount,
                PolicyAccountId = policyId,
                IsPaid = true
            };
            _installmentRepository.Add(firstInstallment);
            //List<PolicyInstallment> installments = new List<PolicyInstallment>();
            for (int i = 1; i < totalInstallments; i++)//i=1
            {
                DateTime installmentDate = startDate.AddMonths(i * intervalInMonths);

                var installment = new PolicyInstallment
                {
                    Id = Guid.NewGuid(),
                    InstallmentPaidDate = installmentDate,
                    Amount = installmentAmount,
                    PolicyAccountId = policyId,
                    IsPaid = false
                };

                _installmentRepository.Add(installment);

            }

        }
        //public void BuyPolicy(Guid customerId, PolicyAccountDto policyAccountDto, PolicyDto policyDto)
        //{
        //    var policyAccount = _mapper.Map<PolicyAccount>(policyAccountDto);
        //    policyAccount.CustomerId = customerId;
        //    policyAccount.Status = "Active";
        //    policyAccount.StartDate = DateTime.Now;
        //    policyAccount.EndDate = DateTime.Now.AddYears(policyDto.MaximumPolicyTerm);
        //    _policyAccountRepository.Add(policyAccount);
        //}
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
        public ICollection<PolicyDto> GetAvailablePolicies()
        {
            var policies = _policyRepository.GetAll().ToList();
            return _mapper.Map<List<PolicyDto>>(policies);
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

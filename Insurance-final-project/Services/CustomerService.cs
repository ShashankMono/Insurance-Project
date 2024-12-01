using Insurance_final_project.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Insurance_final_project.Dto;
using Insurance_final_project.Repositories;
using Microsoft.EntityFrameworkCore;

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
            policyAccount.Status = "Open";
            policyAccount.StartDate = DateTime.UtcNow;
            policyAccount.EndDate = DateTime.UtcNow.AddYears(policyAccount.Policy.MaximumPolicyTerm);

            _policyAccountRepository.Add(policyAccount);

            var customer = _customerRepository.Get(policyAccount.CustomerId);
            if (customer.PolicyAccounts == null)
                customer.PolicyAccounts = new List<PolicyAccount>();

            customer.PolicyAccounts.Add(policyAccount);
            _customerRepository.Update(customer);

            return policyAccount.Id;
        }
        public void BuyPolicy(Guid customerId, PolicyAccountDto policyAccountDto, PolicyDto policyDto)
        {
            var policyAccount = _mapper.Map<PolicyAccount>(policyAccountDto);
            policyAccount.CustomerId = customerId;
            policyAccount.Status = "Active";
            policyAccount.StartDate = DateTime.Now;
            policyAccount.EndDate = DateTime.Now.AddYears(policyDto.MaximumPolicyTerm);
            _policyAccountRepository.Add(policyAccount);
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

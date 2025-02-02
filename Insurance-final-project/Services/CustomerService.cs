﻿using Insurance_final_project.Models;
using AutoMapper;
using Insurance_final_project.Dto;
using Insurance_final_project.Repositories;
using Microsoft.EntityFrameworkCore;
using Insurance_final_project.Constant;
using Insurance_final_project.Exceptions;

namespace Insurance_final_project.Services
{
    public class CustomerService:ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<State> _stateRepo;
        private readonly IRepository<City> _cityRepo;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IRepository<Document> _documentRepo;

        public CustomerService(
            IRepository<Customer> customerRepository,
            IMapper mapper
            , IRepository<City> city
            , IRepository<State> stateRepo
            , IRepository<User> userRepo
            , IEmailService emailService
            ,IRepository<Document> documentRepo)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _cityRepo = city;
            _stateRepo = stateRepo;
            _userRepo = userRepo;
            _emailService = emailService;
            _documentRepo = documentRepo;
        }

        public CustomerProfileDto GetCustomerById(Guid customerId)
        {
            var customer = _customerRepository.GetAll()
                .Include(c => c.PolicyAccounts)
                .Include(c => c.City)
                .Include(c => c.State)
                .FirstOrDefault(c => c.CustomerId == customerId);

            if (customer == null)
                throw new CustomerNotFoundException("Customer not found");

            return _mapper.Map<CustomerProfileDto>(customer);
        }



        public bool ApproveCustomer(ApprovalDto approval)
        {
            var existingCustomer = _customerRepository.GetAll().AsNoTracking().FirstOrDefault(c => c.CustomerId == approval.Id);
            if (existingCustomer == null)
            {
                throw new CustomerNotFoundException("Customer not found!");
            }

            if(approval.IsApproved.ToLower() == ApprovalType.Approved.ToString().ToLower())
            {
                var documents = _documentRepo.GetAll().AsNoTracking().Where(d=>d.CustomerId == approval.Id).ToList();
                foreach (var document in documents) { 
                    if(document.IsVerified != VerificationType.Verified.ToString())
                    {
                        throw new AllDocumentNotVerifiedException("Please verify all document!");
                    }
                }
            }

            existingCustomer.IsApproved = approval.IsApproved.ToLower()=="Approved".ToLower() || approval.IsApproved == "Approve".ToLower()
                                                            ?ApprovalType.Approved.ToString():ApprovalType.Rejected.ToString();
            if (approval.IsApproved.ToLower() == ApprovalType.Rejected.ToString().ToLower())
            {
                _emailService.RejectionMail(approval.Id, approval.Reason,
                    $"{existingCustomer.FirstName+" "+existingCustomer.LastName} Kyc rejected");
            }
            else if (approval.IsApproved.ToLower() == ApprovalType.Approved.ToString().ToLower())
            {
                var mail = existingCustomer.EmailId;
                var Subject = $"Status update of Account kyc";
                var message = $"Kyc Approved! {existingCustomer.FirstName + " " + existingCustomer.LastName}";
                _emailService.ApprovalOrVrifiedMail(mail, Subject, message);
            }
            _customerRepository.Update(existingCustomer);
            return true;
        }


        public bool UpdateProfile(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            check(customerDto);
            var existingCustomer = _customerRepository.GetAll().AsNoTracking().FirstOrDefault(c => c.CustomerId == customer.CustomerId);
            if (existingCustomer == null)
            {
                throw new CustomerNotFoundException("Customer not found!");
            }
            customer.UserId = existingCustomer.UserId;
            _customerRepository.Update(customer);
            return true;
        }
        public void check(CustomerDto customerDto)
        {
            if (_userRepo.Get(customerDto.UserId) == null)
            {
                throw new InvalidGuidException("User not found!");
            }
            else if (_stateRepo.Get(customerDto.StateId) == null)
            {
                throw new InvalidGuidException("State not found!");
            }
            else if (_cityRepo.Get(customerDto.CityId) == null)
            {
                throw new InvalidGuidException("City not found!");
            }
        }
        public CustomerDto RegisterCustomer(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            check(customerDto);
            customer = _customerRepository.Add(customer);
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<List<CustomerProfileDto>> GetCustomerAccounts(string searchQuery)
        {
            var query = _customerRepository.GetAll()
                .Include(c => c.City)
                .Include(c => c.State)
                .Include(c=>c.Documents)
                .OrderByDescending(c=>c.CustomerId)
                .AsQueryable();
           

            if (!string.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.ToLower().Trim();
                query = query.Where(c => (c.FirstName + " " + c.LastName).ToLower() == searchQuery ||
                    c.IsApproved.ToLower() == searchQuery ||
                    c.City.CityName.ToLower() == searchQuery ||
                    c.State.StateName.ToLower() == searchQuery ||
                    c.MobileNo.ToLower() == searchQuery ||
                    c.EmailId.ToLower() == searchQuery
                    
                );
            }
                return _mapper.Map<List<Customer>, List<CustomerProfileDto>>(query.ToList());
        }
        public CustomerProfileDto GetCustomerByUserId(Guid UserId)
        {
            var customer = _customerRepository.GetAll()
                .Include(c => c.PolicyAccounts)
                .Include(c => c.City)
                .Include(c => c.State)
                .FirstOrDefault(c => c.UserId == UserId);

            if (customer == null)
                throw new CustomerNotFoundException("User not found");

            return _mapper.Map<CustomerProfileDto>(customer);
        }
    }
}

using Insurance_final_project.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Insurance_final_project.Dto;
using Insurance_final_project.Repositories;
using Microsoft.EntityFrameworkCore;
using Insurance_final_project.Data;
using Insurance_final_project.Constant;
using Insurance_final_project.Exceptions;

namespace Insurance_final_project.Services
{
    public class CustomerService:ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(
            IRepository<Customer> customerRepository,
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public CustomerDto GetCustomerById(Guid customerId)
        {
            var customer = _customerRepository.GetAll()
                .Include(c => c.PolicyAccounts)
                .Include(c => c.City)
                .Include(c => c.State)
                .FirstOrDefault(c => c.CustomerId == customerId);

            if (customer == null)
                throw new CustomerNotFoundException("Customer not found");

            return _mapper.Map<CustomerDto>(customer);
        }

        public bool UpdateProfile(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            var existingCustomer = _customerRepository.GetAll().AsNoTracking().FirstOrDefault(c=>c.CustomerId ==customer.CustomerId);
            if (existingCustomer == null)
            {
                throw new CustomerNotFoundException("Customer not found!");
            }
            customer.UserId = existingCustomer.UserId;  
            _customerRepository.Update(customer);
             return true;
        }
        
        public CustomerDto RegisterCustomer(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            customer = _customerRepository.Add(customer);
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<List<CustomerDto>> GetCustomerAccounts()
        {
            return _mapper.Map<List<Customer>, List<CustomerDto>>(_customerRepository.GetAll()
                .Include(c => c.Transactions)
                .Include(c => c.PolicyAccounts)
                .Include(c => c.Queries)
                .ToList());
        }
    }
}

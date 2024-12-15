using AutoMapper;
using Insurance_final_project.Dto;
using Insurance_final_project.Exceptions;
using Insurance_final_project.Models;
using Insurance_final_project.Repositories;
using MailKit.Search;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;

namespace Insurance_final_project.Services
{
    public class TransactionService:ITransactionService
    {
        private readonly IRepository<Transaction> _TransactionRepo;
        private readonly IMapper _Mapper;
        private readonly IRepository<Customer> _customerRepo;
        private readonly IRepository<PolicyAccount> _policyAccountRepo;
        private readonly IRepository<PolicyInstallment> _installmentRepo;
        public TransactionService(IRepository<Transaction> repo
            , IRepository<Customer> cutomer
            , IMapper mapper,
            IRepository<PolicyAccount> policyAccountRepo
            , IRepository<PolicyInstallment> installmentRepo)
        {
            _TransactionRepo = repo;
            _Mapper = mapper;
            _customerRepo = cutomer;
            _policyAccountRepo = policyAccountRepo;
            _installmentRepo = installmentRepo;
        }

        public async Task<Guid> AddTransaction(TransactionDto transactionDto)
        {
            if (_customerRepo.GetAll().AsNoTracking().FirstOrDefault(c => c.CustomerId == transactionDto.CustomerId) == null)
            {
                throw new InvalidGuidException("Customer not found!");
            }
            else if (_policyAccountRepo.Get(transactionDto.PolicyAccountId) == null)
            {
                throw new InvalidGuidException("Account not found!");
            }
            else if (_installmentRepo.Get(transactionDto.PolicyInstallmentId) == null)
            {
                throw new InvalidGuidException("Installment not found!");
            }
            return _TransactionRepo.Add(_Mapper.Map<Transaction>(transactionDto)).Id;
        }

        public async Task<List<TransactionDto>> GetTransactionByCustomerId(Guid customerId,
            string? searchQuery,
            DateTime? startDate,
            DateTime? endDate)
        {
            if (_customerRepo.Get(customerId) == null)
            {
                throw new CustomerNotFoundException("Customer Not found!");
            }

            var query = _TransactionRepo.GetAll()
                .Include(t => t.PolicyAccount).ThenInclude(pa => pa.Policy)
                .Where(t=>t.CustomerId == customerId)
                .OrderByDescending(t=>t.Id)
            .AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.ToLower();
                query = query.Where(t => t.PolicyAccount.Policy.Name.ToLower() == searchQuery || 
                    t.ReferenceNumber.ToString().ToLower() == searchQuery ||
                    t.Type.ToLower() == searchQuery
                );
            }
            if (startDate.HasValue)
            {
                query = query.Where(t => t.DateTime.Date >= startDate.Value.Date);
            }
            if (endDate.HasValue)
            {
                query = query.Where(t => t.DateTime.Date <= endDate.Value.Date);
            }

            return _Mapper.Map<List<Transaction>, List<TransactionDto>>(query.ToList());
        }

        public async Task<List<TransactionDto>> GetTransactions(string? searchQuery,
            DateTime? startDate,
            DateTime? endDate)
        {
            var query = _TransactionRepo.GetAll()
                .Include(t => t.PolicyAccount).ThenInclude(pa => pa.Policy)
                .Include(t => t.Customer)
                .OrderByDescending(t => t.Id)
                .AsQueryable();

            if(!string.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.ToLower();
                query = query.Where(t=>t.PolicyAccount.Policy.Name.ToLower() == searchQuery ||
                    (t.Customer.FirstName+" "+t.Customer.LastName).ToLower() == searchQuery ||
                    t.Type.ToLower() == searchQuery
                );
            }
            if (startDate.HasValue)
            {
                query = query.Where(t=>t.DateTime.Date >=  startDate.Value.Date);
            }
            if (endDate.HasValue) 
            { 
                query=query.Where(t=>t.DateTime.Date <= endDate.Value.Date);
            }

            return _Mapper.Map<List<Transaction>, List<TransactionDto>>(query.ToList());
        }

    }
}

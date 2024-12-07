using AutoMapper;
using Insurance_final_project.Dto;
using Insurance_final_project.Exceptions;
using Insurance_final_project.Models;
using Insurance_final_project.Repositories;
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
            ,IRepository<Customer> cutomer
            , IMapper mapper,
            IRepository<PolicyAccount> policyAccountRepo
            ,IRepository<PolicyInstallment> installmentRepo)
        {
            _TransactionRepo = repo;
            _Mapper = mapper;
            _customerRepo = cutomer;
            _policyAccountRepo = policyAccountRepo;
            _installmentRepo = installmentRepo;
        }

        public async Task<Guid> AddTransaction(TransactionDto transactionDto)
        {
            if(_customerRepo.GetAll().AsNoTracking().FirstOrDefault(c=>c.CustomerId == transactionDto.CustomerId) == null)
            {
                throw new InvalidGuidException("Customer not found!");
            }else if(_policyAccountRepo.Get(transactionDto.PolicyAccountId) == null)
            {
                throw new InvalidGuidException("Account not found!");
            }
            else if (_installmentRepo.Get(transactionDto.PolicyInstallmentId) == null)
            {
                throw new InvalidGuidException("Installment not found!");
            }
            return _TransactionRepo.Add(_Mapper.Map<Transaction>(transactionDto)).Id;
        }

        public async Task<List<TransactionDto>> GetTransactionByCustomerId(Guid customerId)
        {
            if(_customerRepo.Get(customerId) == null)
            {
                throw new CustomerNotFoundException("Customer Not found!");
            }
            return _Mapper.Map<List<TransactionDto>>(_TransactionRepo.GetAll().Where(t=>t.CustomerId == customerId).ToList());
        }

        public async Task<List<TransactionDto>> GetTransactions()
        {
            return _Mapper.Map<List<Transaction>, List<TransactionDto>>(_TransactionRepo.GetAll().ToList());
        }

    }
}

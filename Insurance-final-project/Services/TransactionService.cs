using AutoMapper;
using Insurance_final_project.Dto;
using Insurance_final_project.Exceptions;
using Insurance_final_project.Models;
using Insurance_final_project.Repositories;

namespace Insurance_final_project.Services
{
    public class TransactionService:ITransactionService
    {
        private readonly IRepository<Transaction> _TransactionRepo;
        private readonly IMapper _Mapper;
        private readonly IRepository<Customer> _customerRepo;
        public TransactionService(IRepository<Transaction> repo,IRepository<Customer> cutomer, IMapper mapper)
        {
            _TransactionRepo = repo;
            _Mapper = mapper;
            _customerRepo = cutomer;
        }

        public async Task<Guid> AddTransaction(TransactionDto transactionDto)
        {
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

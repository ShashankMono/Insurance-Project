using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface ITransactionService
    {
        public Task<List<TransactionDto>> GetTransactions();
        public Task<Guid> AddTransaction(TransactionDto transactionDto);
        public Task<List<TransactionDto>> GetTransactionByCustomerId(Guid customerId);
    }
}

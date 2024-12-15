using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface ITransactionService
    {
        public Task<List<TransactionDto>> GetTransactions(string? searchQuery,
            DateTime? startDate,
            DateTime? endDate);
        public Task<Guid> AddTransaction(TransactionDto transactionDto);
        public Task<List<TransactionDto>> GetTransactionByCustomerId(Guid customerId, string? searchQuery,
            DateTime? startDate,
            DateTime? endDate);
    }
}

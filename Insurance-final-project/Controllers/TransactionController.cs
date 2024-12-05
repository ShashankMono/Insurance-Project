using Insurance_final_project.Dto;
using Insurance_final_project.Exceptions;
using Insurance_final_project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_final_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // Add a new transaction
        [HttpPost]
        public async Task<IActionResult> AddTransaction([FromBody] TransactionDto transactionDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new
                {
                    Success = false,
                    Data = (object)null,
                    Message = "Validation failed.",
                    Errors = errors
                });
            }
            var transactionId = await _transactionService.AddTransaction(transactionDto);
            return Ok(new
            {
                Success = true,
                Data = transactionId,
                Message = "Transaction added successfully."
            });
        }

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetTransactionByCustomerId(Guid customerId)
        {
            try
            {
                var transactions = await _transactionService.GetTransactionByCustomerId(customerId);
                return Ok(new
                {
                    Success = true,
                    Data = transactions,
                    Message = "Transactions retrieved successfully."
                });
            }
            catch (CustomerNotFoundException ex)
            {
                return NotFound(new { Success = false, Message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {
            var transactions = await _transactionService.GetTransactions();
            return Ok(new
            {
                Success = true,
                Data = transactions,
                Message = "All transactions retrieved successfully."
            });
        }
    }
}

using Insurance_final_project.Dto;
using Insurance_final_project.Exceptions;
using Insurance_final_project.Models;
using Insurance_final_project.PagingFiles;
using Insurance_final_project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

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
        public async Task<IActionResult> GetTransactionByCustomerId(Guid customerId, 
            [FromQuery] PageParameters pageParameter,
            [FromQuery]string? searchQuery,
            [FromQuery]DateTime? startDate,
            [FromQuery] DateTime? endDate)
        {
                var transactions = await _transactionService.GetTransactionByCustomerId(customerId,
                    searchQuery,
                    startDate,
            endDate);

            var pagedData = PageList<TransactionDto>.ToPagedList(transactions, pageParameter.PageNumber, pageParameter.PageSize);

            return Ok(new
                {
                    Success = true,
                    Data = pagedData,
                    totalItems = pagedData.TotalCount,
                    pageNumber = pagedData.CurrentPage,
                    pagesize = pagedData.PageSize,
                    totalPages = pagedData.TotalPages,
                Message = "Transactions retrieved successfully."
                });
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions([FromQuery] PageParameters pageParameter,
            [FromQuery] string? searchQuery,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)
        {
            var transactions = await _transactionService.GetTransactions(
                searchQuery,
                startDate,
                endDate);

            var pagedData = PageList<TransactionDto>.ToPagedList(transactions, pageParameter.PageNumber, pageParameter.PageSize);

            return Ok(new
            {
                Success = true,
                Data = pagedData,
                totalItems = pagedData.TotalCount,
                pageNumber = pagedData.CurrentPage,
                pagesize = pagedData.PageSize,
                totalPages = pagedData.TotalPages,
                Message = "Transactions retrieved successfully."
            });
        }
    }
}

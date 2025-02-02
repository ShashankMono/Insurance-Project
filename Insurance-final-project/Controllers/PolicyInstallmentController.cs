﻿using Insurance_final_project.Dto;
using Insurance_final_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_final_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyInstallmentController : ControllerBase
    {
        private readonly IPolicyInstallmentService _policyInstallmentService;

        public PolicyInstallmentController(IPolicyInstallmentService policyInstallmentService)
        {
            _policyInstallmentService = policyInstallmentService;
        }

        [HttpGet("add/{policyAccountId}"), Authorize(Roles = "Employee")]
        public IActionResult AddInstallments(Guid policyAccountId)
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

            _policyInstallmentService.AddInstallments(policyAccountId);

            return Ok(new
            {
                Success = true,
                Data = (object)null,
                Message = "Installments added successfully."
            });
        }

        [HttpGet("pay/{InstallmentId}"), Authorize(Roles = "Customer")]
        public async Task<IActionResult> PayInstallment(Guid InstallmentId)
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

            var result = await _policyInstallmentService.PayInstallment(InstallmentId);

            return Ok(new
            {
                Success = true,
                Data = result,
                Message = "Installment payment successful."
            });
        }


        [HttpGet("policyaccount/{policyAccountId}"), Authorize(Roles = "Admin,Customer,Employee")]
        public async Task<IActionResult> GetInstallmentsByPolicyAccountId(Guid policyAccountId)
        {
            var installments = await _policyInstallmentService.GetInstallmentsByPolicyAccountId(policyAccountId);

            return Ok(new
            {
                Success = true,
                Data = installments,
                Message = "Installments retrieved successfully."
            });
        }
    }
}

using Insurance_final_project.Dto;
using Insurance_final_project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService; 

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        private void ValidateModel()
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException("Invalid model state.");
            }
        }

        [HttpPost("add-agent")]
        public IActionResult AddAgent([FromBody] AgentDto newAgent)
        {
            ValidateModel(); 

            var user = _adminService.AddAgent(newAgent);
            return Ok(new { Success = true, Data = user, Message = "Agent added successfully." });
        }

        [HttpPost("add-employee")]
        public IActionResult AddEmployee([FromBody] EmployeeDto newEmployee)
        {
            ValidateModel();

            var user = _adminService.AddEmployee(newEmployee);
            return Ok(new { Success = true, Data = user, Message = "Employee added successfully." });
        }

        [HttpPost("add-policy")]
        public IActionResult AddPolicy([FromBody] PolicyDTO policy)
        {
            ValidateModel(); 

            var policyId = _adminService.AddPolicy(policy);
            return Ok(new { Success = true, Data = policyId, Message = "Policy added successfully." });
        }

        [HttpPost("add-policy-type")]
        public IActionResult AddPolicyType([FromBody] PolicyTypeDto policyType)
        {
            ValidateModel(); 

            var policyTypeId = _adminService.AddPolicyType(policyType);
            return Ok(new { Success = true, Data = policyTypeId, Message = "Policy type added successfully." });
        }

        [HttpPost("add-role")]
        public IActionResult AddRole([FromBody] RoleDto role)
        {
            ValidateModel(); 

            var roleId = _adminService.AddRole(role);
            return Ok(new { Success = true, Data = roleId, Message = "Role added successfully." });
        }

        [HttpPost("approve-customer")]
        public IActionResult ApproveCustomer([FromBody] CustomerDto customer)
        {
            ValidateModel(); 

            var customerId = _adminService.ApproveCustomer(customer);
            return Ok(new { Success = true, Data = customerId, Message = "Customer approved successfully." });
        }

        [HttpPost("approve-policy-cancellation")]
        public IActionResult ApprovePolicyCancellation([FromBody] PolicyCancelDto policyCancel)
        {
            ValidateModel(); 

            var policyCancelId = _adminService.ApprovePolicyCancelation(policyCancel);
            return Ok(new { Success = true, Data = policyCancelId, Message = "Policy cancellation approved successfully." });
        }

        [HttpPost("approve-withdrawal")]
        public IActionResult ApproveWithdrawal([FromBody] CommissionWithdrawalDto withdrawRequest)
        {
            ValidateModel(); 

            var withdrawalId = _adminService.ApproveWithdrawal(withdrawRequest);
            return Ok(new { Success = true, Data = withdrawalId, Message = "Withdrawal approved successfully." });
        }

        [HttpPost("add-city")]
        public IActionResult AddCity([FromBody] CityDto city)
        {
            ValidateModel(); 

            var cityId = _adminService.AddCity(city);
            return Ok(new { Success = true, Data = cityId, Message = "City added successfully." });
        }

        [HttpPut("update-city")]
        public IActionResult UpdateCity([FromBody] CityDto city)
        {
            ValidateModel(); 

            var cityId = _adminService.UpdateCity(city);
            return Ok(new { Success = true, Data = cityId, Message = "City updated successfully." });
        }

        [HttpPost("claim-approval")]
        public IActionResult ClaimApproval([FromBody] ClaimDto claim)
        {
            ValidateModel(); 

            var claimId = _adminService.ClaimApproval(claim);
            return Ok(new { Success = true, Data = claimId, Message = "Claim approved successfully." });
        }

        [HttpPost("deactivate-user")]
        public IActionResult DeActivateUser([FromBody] ChangeUserStatusDto userStatus)
        {
            ValidateModel(); 

            var userId = _adminService.DeActivateUser(userStatus);
            return Ok(new { Success = true, Data = userId, Message = "User deactivated successfully." });
        }

        [HttpGet("get-agent-report")]
        public IActionResult GetAgentReport([FromQuery] AgentDto agent)
        {
            ValidateModel(); 

            var agentReport = _adminService.GetAgentReport(agent);
            return Ok(new { Success = true, Data = agentReport, Message = "Agent report retrieved successfully." });
        }

        [HttpGet("get-claim-accounts")]
        public IActionResult GetClaimAccounts()
        {
            var claimAccounts = _adminService.GetClaimAccounts();
            return Ok(new { Success = true, Data = claimAccounts, Message = "Claim accounts retrieved successfully." });
        }

        [HttpGet("get-commissions")]
        public IActionResult GetCommissions()
        {
            var commissions = _adminService.GetCommissions();
            return Ok(new { Success = true, Data = commissions, Message = "Commissions retrieved successfully." });
        }

        [HttpGet("get-commissions-withdrawal")]
        public IActionResult GetCommissionsWithdrawal()
        {
            var commissionWithdrawals = _adminService.GetCommissionsWithdrawal();
            return Ok(new { Success = true, Data = commissionWithdrawals, Message = "Commission withdrawals retrieved successfully." });
        }

        [HttpGet("get-customer-accounts")]
        public IActionResult GetCustomerAccounts()
        {
            var customerAccounts = _adminService.GetCustomerAccounts();
            return Ok(new { Success = true, Data = customerAccounts, Message = "Customer accounts retrieved successfully." });
        }

        [HttpGet("get-policy-account")]
        public IActionResult GetPolicyAccount([FromQuery] PolicyAccountDto policyAccount)
        {
            var policyAccountDetails = _adminService.GetPolicyAccount(policyAccount);
            return Ok(new { Success = true, Data = policyAccountDetails, Message = "Policy account retrieved successfully." });
        }

        [HttpGet("get-all-policy-accounts")]
        public IActionResult GetAllPolicyAccounts()
        {
            var policyAccounts = _adminService.GetAllPolicyAccounts();
            return Ok(new { Success = true, Data = policyAccounts, Message = "All policy accounts retrieved successfully." });
        }

        [HttpGet("get-policy-cancels")]
        public IActionResult GetPolicyCancels()
        {
            var policyCancels = _adminService.GetPolicyCancels();
            return Ok(new { Success = true, Data = policyCancels, Message = "Policy cancels retrieved successfully." });
        }

        [HttpGet("get-transactions")]
        public IActionResult GetTransactions()
        {
            var transactions = _adminService.GetTransactions();
            return Ok(new { Success = true, Data = transactions, Message = "Transactions retrieved successfully." });
        }

        [HttpPost("add-state")]
        public IActionResult AddState([FromBody] StateDto state)
        {
            ValidateModel(); 

            var stateId = _adminService.AddState(state);
            return Ok(new { Success = true, Data = stateId, Message = "State added successfully." });
        }

        [HttpPut("update-policy")]
        public IActionResult UpdatePolicy([FromBody] PolicyDTO policy)
        {
            ValidateModel(); 

            var policyId = _adminService.UpdatePolicy(policy);
            return Ok(new { Success = true, Data = policyId, Message = "Policy updated successfully." });
        }

        [HttpPut("update-state")]
        public IActionResult UpdateState([FromBody] StateDto state)
        {
            ValidateModel();

            var stateId = _adminService.UpdateState(state);
            return Ok(new { Success = true, Data = stateId, Message = "State updated successfully." });
        }
    }
}

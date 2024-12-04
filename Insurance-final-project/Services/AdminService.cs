﻿using AutoMapper;
using Insurance_final_project.Constant;
using Insurance_final_project.Dto;
using Insurance_final_project.Exceptions;
using Insurance_final_project.Models;
using Insurance_final_project.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Insurance_final_project.Services
{
    public class AdminService : IAdminService
    {
        private readonly IRepository<Agent> _AgentRepo;
        private readonly IRepository<Employee> _EmployeeRepo;
        private readonly IRepository<Policy> _PolicyRepo;
        private readonly IRepository<Role> _RoleRepo;
        private readonly IRepository<User> _UserRepo;
        private readonly IRepository<Customer> _CustomerRepo;
        private readonly IRepository<PolicyType> _PolicyTypeRepo;
        private readonly IRepository<Commission> _CommissionRepo;
        private readonly IRepository<CommissionWithdrawal> _CommissionWithdrawalRepo;
        private readonly IRepository<City> _CityRepo;
        private readonly IRepository<State> _StateRepo;
        private readonly IRepository<Claim> _ClaimRepo;
        private readonly IRepository<PolicyAccount> _PolicyAccountRepo;
        private readonly IRepository<PolicyCancel> _PolicyCancelRepo;
        private readonly IRepository<Transaction>  _TransactionRepo;
        private readonly IMapper _Mapper;
        public AdminService(
        IRepository<Agent> agentRepo,
        IRepository<Employee> employeeRepo,
        IRepository<Policy> policyRepo,
        IRepository<Role> roleRepo,
        IRepository<User> userRepo,
        IRepository<Customer> customerRepo,
        IRepository<PolicyType> policyTypeRepo,
        IRepository<Commission> commissionRepo,
        IRepository<CommissionWithdrawal> commissionWithdrawalRepo,
        IRepository<City> cityRepo,
        IRepository<State> stateRepo,
        IRepository<Claim> claimRepo,
        IRepository<PolicyAccount> policyAccountRepo,
        IRepository<PolicyCancel> policyCancelRepo,
        IRepository<Transaction> transactionRepo,
        IMapper mapper)
        {
            _AgentRepo = agentRepo;
            _EmployeeRepo = employeeRepo;
            _PolicyRepo = policyRepo;
            _RoleRepo = roleRepo;
            _UserRepo = userRepo;
            _CustomerRepo = customerRepo;
            _PolicyTypeRepo = policyTypeRepo;
            _CommissionRepo = commissionRepo;
            _CommissionWithdrawalRepo = commissionWithdrawalRepo;
            _CityRepo = cityRepo;
            _StateRepo = stateRepo;
            _ClaimRepo = claimRepo;
            _PolicyAccountRepo = policyAccountRepo;
            _PolicyCancelRepo = policyCancelRepo;
            _TransactionRepo = transactionRepo;
            _Mapper = mapper;
        }

        public UserDto AddNewUser(Guid roleId)
        {
            var newUsername = Guid.NewGuid().ToString();
            Random random = new Random();
            var password = random.Next(1001, 10000).ToString();
            UserDto user = new UserDto()
            {
                Username = newUsername,
                Password = password,
                RoleId = roleId,
            };
            User userAgent = _UserRepo.Add(_Mapper.Map<UserDto, User>(user));
            user.UserId = userAgent.UserId;
            return user;
        }
        public async Task<UserDto> AddAgent(AgentInputDto newAgent)
        {
            Agent agent = _Mapper.Map<AgentInputDto,Agent>(newAgent);
            UserDto user = AddNewUser(_RoleRepo.GetAll().FirstOrDefault(r => r.RoleName == "Agent").RoleId);
            agent.UserId = user.UserId;
            Agent agentAdded = _AgentRepo.Add(agent);
            return user;
        }

        public async Task<UserDto> AddEmployee(EmployeeDto newEmployee)
        {
            Employee employee = _Mapper.Map<EmployeeDto, Employee>(newEmployee);
            UserDto user = AddNewUser(_RoleRepo.GetAll().FirstOrDefault(r => r.RoleName == "Employee").RoleId);
            employee.UserId = user.UserId;
            Employee employeeAdded = _EmployeeRepo.Add(employee);
            return user;
        }

        public async Task<Guid> AddPolicy(PolicyDto policy)
        {
            var newPolicy = _Mapper.Map<PolicyDto, Policy>(policy);
            Policy policyAdded = _PolicyRepo.Add(newPolicy);
            return policyAdded.Id;
        }

        public async Task<Guid> AddPolicyType(PolicyTypeDto policyType)
        {
            var newPolicyType = _Mapper.Map<PolicyTypeDto, PolicyType>(policyType);
            if(_PolicyTypeRepo.GetAll().FirstOrDefault(p=>p.Type.ToLower() == newPolicyType.Type.ToLower()) != null)
            {
                throw new PolicyTypeExistException("Type of policy already exist!");
            }
            PolicyType policyTypeAdded = _PolicyTypeRepo.Add(newPolicyType);
            return policyTypeAdded.Id;
        }

        public async Task<Guid> AddRole(RoleDto role)
        {
            var newRole = _Mapper.Map<RoleDto, Role>(role);
            Role roleAdded = _RoleRepo.Add(newRole);
            return roleAdded.RoleId;
        }

        public async Task<Guid> ApproveCustomer(CustomerDto customer)
        {
            Customer updateCustomer = _Mapper.Map<CustomerDto, Customer>(customer);
            Customer updatedCustomer = _CustomerRepo.Update(updateCustomer);
            return updatedCustomer.CustomerId;
        }

        public async Task<Guid> ApprovePolicyCancelation(ApprovalDto policyCancel)
        {
            var policyCan = _PolicyCancelRepo.Get(policyCancel.Id);
            if(policyCan.IsApproved == ApprovalType.Approved.ToString())
            {
                var policyAccount = _PolicyAccountRepo.GetAll().FirstOrDefault(pa=>pa.Id == policyCan.PolicyAccountId);
                if (policyAccount != null) throw new PolicyAccountNotFountException("Policy Account not found!");
                policyAccount.Status = PolicyAccountStatus.Closed.ToString();
                _PolicyAccountRepo.Update(policyAccount);
            }
            policyCan.IsApproved = policyCancel.IsApproved;
            PolicyCancel updatedPolicyCancel = _PolicyCancelRepo.Update(policyCan);
            
            return updatedPolicyCancel.PolicyCancelId;
        }

        public async Task<Guid> ApproveWithdrawal(CommissionWithdrawalDto withdrawRequest)
        {
            var updateWithdrawRequestStatus = _Mapper.Map<CommissionWithdrawalDto, CommissionWithdrawal>(withdrawRequest);
            CommissionWithdrawal updatedWithdrawRequestStatus = _CommissionWithdrawalRepo.Update(updateWithdrawRequestStatus);
            return updatedWithdrawRequestStatus.Id;
        }

        public async Task<Guid> AddCity(CityInputDto city)
        {
            return _CityRepo.Add(_Mapper.Map<CityInputDto, City>(city)).CityId;
        }
        public async Task<Guid> UpdateCity(CityInputDto city)
        {
            return _CityRepo.Update(_Mapper.Map<CityInputDto, City>(city)).CityId;
        }

        public async Task<Guid> ClaimApproval(ApprovalDto claim)
        {
            var claimAccount = _ClaimRepo.GetAll().FirstOrDefault(c=>c.ClaimId == claim.Id);
            claimAccount.ApprovedStatus = claim.IsApproved;
            return _ClaimRepo.Update(claimAccount).ClaimId;
        }

        public async Task<Guid> DeActivateUser(ChangeUserStatusDto user)//changes has been done in this function
        {
            User existingUser = _UserRepo.Get(user.UserId); 
            existingUser.IsActive = user.IsActive;
            return _UserRepo.Update(existingUser).UserId;
        }

        public async Task<AgentResponseDto> GetAgentReport(AgentInputDto agent)
        {
            return _Mapper.Map<Agent, AgentResponseDto>(_AgentRepo.GetAll()
                .Include(a => a.PolicyAccounts)
                .Include(a => a.Commissions)
                .Include(a => a.CommissionWithdrawal)
                .FirstOrDefault(a => a.AgentId == agent.AgentId));
        }

        public async Task<List<ClaimDto>> GetClaimAccounts()
        {
            return _Mapper.Map<List<Claim>, List<ClaimDto>>(_ClaimRepo.GetAll().Include(c=>c.Document).ToList());
        }

        public async Task<List<CommissionDto>> GetCommissions()
        {
            return _Mapper.Map<List<Commission>, List<CommissionDto>>(_CommissionRepo.GetAll().ToList());
        }

        public async Task<List<CommissionWithdrawalDto>> GetCommissionsWithdrawal()
        {
            return _Mapper.Map<List<CommissionWithdrawal>, List<CommissionWithdrawalDto>>(_CommissionWithdrawalRepo.GetAll().ToList());
        }

        public async Task<List<CustomerDto>> GetCustomerAccounts()
        {
            return _Mapper.Map<List<Customer>, List<CustomerDto>>(_CustomerRepo.GetAll()
                .Include(c=>c.Transactions)
                .Include(c=>c.PolicyAccounts)
                .Include(c=>c.Queries)
                .ToList());
        }

        public async Task<PolicyAccountDto> GetPolicyAccount(PolicyAccountDto policyAccount)
        {
            return _Mapper.Map<PolicyAccount, PolicyAccountDto>(_PolicyAccountRepo.GetAll()
                .Include(p=>p.Policy)
                .Include(p=>p.policyInstallments)
                .ThenInclude(installment=>installment.Transaction)
                .FirstOrDefault(p=>p.Id == policyAccount.Id)
                );
        }

        public async Task<List<PolicyAccountDto>> GetAllPolicyAccounts()
        {
            return _Mapper.Map<List<PolicyAccount>, List<PolicyAccountDto>>(_PolicyAccountRepo.GetAll().ToList());
        }

        public async Task<List<PolicyCancelDto>> GetPolicyCancels()
        {
            return _Mapper.Map<List<PolicyCancel>, List<PolicyCancelDto>>(_PolicyCancelRepo.GetAll().ToList());
        }

        public async Task<List<TransactionDto>> GetTransactions()
        {
            return _Mapper.Map<List<Transaction>,List<TransactionDto>>(_TransactionRepo.GetAll().ToList());
        }

        public async Task<Guid> AddState(StateDto state)
        {
            return _StateRepo.Add(_Mapper.Map<StateDto, State>(state)).StateId;
        }

        public async Task<Guid> UpdatePolicy(PolicyDto policy)
        {
            return _PolicyRepo.Update(_Mapper.Map<PolicyDto, Policy>(policy)).Id;
        }

        public async Task<Guid> UpdateState(StateDto state)
        {
            return _StateRepo.Update(_Mapper.Map<StateDto, State>(state)).StateId;
        }

        public async Task<List<EmployeeDto>> GetAllEmployee()
        {
            return _Mapper.Map<List<Employee>,List<EmployeeDto>>(_EmployeeRepo.GetAll().ToList());
        }

        public async Task<List<AgentInputDto>> GetAllAgents()
        {
            return _Mapper.Map<List<Agent>,List<AgentInputDto>>(_AgentRepo.GetAll().ToList());
        }

        public async Task<List<PolicyDto>> GetPolicies()
        {
            return _Mapper.Map<List<Policy>,List<PolicyDto>>(_PolicyRepo.GetAll().ToList());
        }

        public async Task<PolicyDto> GetPolicy(Guid policyId)
        {
            return _Mapper.Map<Policy,PolicyDto>(_PolicyRepo.GetAll()
                .Include(p=>p.PolicyAccounts)
                .FirstOrDefault(x => x.Id == policyId));
        }
    }
}

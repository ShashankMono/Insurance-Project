﻿using AutoMapper;
using Insurance_final_project.Dto;
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

        public Guid AddNewUser()
        {
            var newUsername = Guid.NewGuid().ToString();
            Random random = new Random();
            var password = random.Next(1001, 10000).ToString();
            UserDto user = new UserDto()
            {
                Username = newUsername,
                Password = password,
                RoleId = _RoleRepo.GetAll().FirstOrDefault(r => r.RoleName == "Agent").RoleId,
            };
            User userAgent = _UserRepo.Add(_Mapper.Map<UserDto, User>(user));
            return userAgent.UserId;
        }
        public Guid AddAgent(AgentDto newAgent)
        {
            Agent agent = _Mapper.Map<AgentDto,Agent>(newAgent);
            agent.UserId = AddNewUser();
            Agent agentAdded = _AgentRepo.Add(agent);
            return agentAdded.AgentId;
        }

        public Guid AddEmployee(EmployeeDto newEmployee)
        {
            Employee employee = _Mapper.Map<EmployeeDto, Employee>(newEmployee);
            employee.UserId = AddNewUser();
            Employee employeeAdded = _EmployeeRepo.Add(employee);
            return employeeAdded.EmployeeId;
        }

        public Guid AddPolicy(PolicyDTO policy)
        {
            var newPolicy = _Mapper.Map<PolicyDTO, Policy>(policy);
            Policy policyAdded = _PolicyRepo.Add(newPolicy);
            return policyAdded.Id;
        }

        public Guid AddPolicyType(PolicyTypeDto policyType)
        {
            var newPolicyType = _Mapper.Map<PolicyTypeDto, PolicyType>(policyType);
            PolicyType policyTypeAdded = _PolicyTypeRepo.Add(newPolicyType);
            return policyTypeAdded.Id;
        }

        public Guid AddRole(RoleDto role)
        {
            var newRole = _Mapper.Map<RoleDto, Role>(role);
            Role roleAdded = _RoleRepo.Add(newRole);
            return roleAdded.RoleId;
        }

        public Guid ApproveCustomer(CustomerDto customer)
        {
            Customer updateCustomer = _Mapper.Map<CustomerDto, Customer>(customer);
            Customer updatedCustomer = _CustomerRepo.Update(updateCustomer);
            return updatedCustomer.CustomerId;
        }

        public Guid ApprovePolicyCancelation(PolicyCancelDto policyCancel)
        {
            var updatePolicyCancelStatus = _Mapper.Map<PolicyCancelDto, PolicyCancel>(policyCancel);
            PolicyCancel updatedPolicyCancel = _PolicyCancelRepo.Update(updatePolicyCancelStatus);
            return updatedPolicyCancel.PolicyCancelId;
        }

        public Guid ApproveWithdrawal(CommissionWithdrawalDto withdrawRequest)
        {
            var updateWithdrawRequestStatus = _Mapper.Map<CommissionWithdrawalDto, CommissionWithdrawal>(withdrawRequest);
            CommissionWithdrawal updatedWithdrawRequestStatus = _CommissionWithdrawalRepo.Update(updateWithdrawRequestStatus);
            return updatedWithdrawRequestStatus.Id;
        }

        public Guid City(CityDto city)
        {
            return _CityRepo.Add(_Mapper.Map<CityDto, City>(city)).CityId;
        }

        public Guid ClaimApproval(ClaimDto claim)
        {
            return _ClaimRepo.Update(_Mapper.Map<ClaimDto, Claim>(claim)).ClaimId;
        }

        public Guid DeActivateUser(UserDto user)
        {
            return _UserRepo.Update(_Mapper.Map<UserDto, User>(user)).UserId;
        }

        public AgentDto GetAgentReport(AgentDto agent)
        {
            return _Mapper.Map<Agent, AgentDto>(_AgentRepo.GetAll()
                .Include(a => a.PolicyAccounts)
                .Include(a => a.Commissions)
                .Include(a => a.CommissionWithdrawal)
                .FirstOrDefault(a => a.AgentId == agent.AgentId));
        }

        public List<ClaimDto> GetClaimAccounts()
        {
            return _Mapper.Map<List<Claim>, List<ClaimDto>>(_ClaimRepo.GetAll().Include(c=>c.Document).ToList());
        }

        public List<CommissionDto> GetCommissions()
        {
            return _Mapper.Map<List<Commission>, List<CommissionDto>>(_CommissionRepo.GetAll().ToList());
        }

        public List<CommissionWithdrawalDto> GetCommissionsWithdrawal()
        {
            return _Mapper.Map<List<CommissionWithdrawal>, List<CommissionWithdrawalDto>>(_CommissionWithdrawalRepo.GetAll().ToList());
        }

        public List<CustomerDto> GetCustomerAccounts()
        {
            return _Mapper.Map<List<Customer>, List<CustomerDto>>(_CustomerRepo.GetAll()
                .Include(c=>c.Transactions)
                .Include(c=>c.PolicyAccounts)
                .Include(c=>c.Queries)
                .ToList());
        }

        public PolicyAccountDto GetPolicyAccount(PolicyAccountDto policyAccount)
        {
            return _Mapper.Map<PolicyAccount, PolicyAccountDto>(_PolicyAccountRepo.GetAll()
                .Include(p=>p.Policy)
                .Include(p=>p.policyInstallments)
                .ThenInclude(installment=>installment.Transaction)
                .FirstOrDefault(p=>p.Id == policyAccount.Id)
                );
        }

        public List<PolicyAccountDto> GetPolicyAccounts()
        {
            return _Mapper.Map<List<PolicyAccount>, List<PolicyAccountDto>>(_PolicyAccountRepo.GetAll().ToList());
        }

        public List<PolicyCancelDto> GetPolicyCancels()
        {
            return _Mapper.Map<List<PolicyCancel>, List<PolicyCancelDto>>(_PolicyCancelRepo.GetAll().ToList());
        }

        public List<TransactionDto> GetTransactions()
        {
            return _Mapper.Map<List<Transaction>,List<TransactionDto>>(_TransactionRepo.GetAll().ToList());
        }

        public Guid AddState(StateDto state)
        {
            return _StateRepo.Add(_Mapper.Map<StateDto, State>(state)).StateId;
        }

        public Guid UpdatePolicy(PolicyDTO policy)
        {
            return _PolicyRepo.Update(_Mapper.Map<PolicyDTO, Policy>(policy)).Id;
        }

        public Guid State(StateDto state)
        {
            throw new NotImplementedException();
        }
    }
}

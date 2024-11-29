using AutoMapper;
using Insurance_final_project.Dto;
using Insurance_final_project.Models;

namespace Insurance_final_project.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            // Admin
            CreateMap<Admin, AdminDto>().ReverseMap();

            // Agent
            CreateMap<Agent, AgentDto>().ReverseMap();

            // City
            CreateMap<City, CityDto>().ReverseMap();

            // Claim
            CreateMap<Claim, ClaimDto>().ReverseMap();

            // Commission Withdrawal
            CreateMap<CommissionWithdrawal, CommissionWithdrawalDto>().ReverseMap();

            // Customer
            CreateMap<Customer, CustomerDto>().ReverseMap();

            // Document
            CreateMap<Document, DocumentDto>().ReverseMap();

            // Employee
            CreateMap<Employee, EmployeeDto>().ReverseMap();

            // Policy
            CreateMap<Policy, PolicyDTO>().ReverseMap();

            // Policy Account
            CreateMap<PolicyAccount, PolicyAccountDto>().ReverseMap();

            // Policy Cancel
            CreateMap<PolicyCancel, PolicyCancelDto>().ReverseMap();

            // Policy Installment
            CreateMap<PolicyInstallment, PolicyInstallmentDto>().ReverseMap();

            // Policy Type
            CreateMap<PolicyType, PolicyTypeDto>().ReverseMap();

            // Query
            CreateMap<Query, QueryDto>().ReverseMap();

            // Role
            CreateMap<Role, RoleDto>().ReverseMap();

            // State
            CreateMap<State, StateDto>().ReverseMap();

            // Transaction
            CreateMap<Transaction, TransactionDto>().ReverseMap();

            // User
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}

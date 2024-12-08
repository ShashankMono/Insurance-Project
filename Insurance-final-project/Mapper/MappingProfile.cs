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
            CreateMap<Agent, AgentInputDto>().ReverseMap();

            // City
            CreateMap<City, CityDto>().ReverseMap();

            // Claim
            CreateMap<Claim, ClaimDto>().ReverseMap();

            // Commission Withdrawal
            CreateMap<CommissionWithdrawal, CommissionWithdrawalDto>().ReverseMap();

            // Agent Commission
            CreateMap<Commission, CommissionDto>().ReverseMap();

            // Customer
            CreateMap<Customer, CustomerDto>().ReverseMap();

            // Document
            CreateMap<Document, DocumentDto>().ReverseMap();

            // Employee
            CreateMap<Employee, EmployeeDto>().ReverseMap();

            // Policy
            CreateMap<Policy, PolicyDto>().ReverseMap();

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

            //CreateMap<>();
            // User
            CreateMap<User, UserDto>();
            CreateMap<UserLoginDto, User>();
            CreateMap<UserDto, User>()
                .ForMember(dest=> dest.Password,val=>val.MapFrom(src=>BCrypt.Net.BCrypt.EnhancedHashPassword(src.Password,13)));

            CreateMap<Agent, AgentInputDto>().ReverseMap();
            CreateMap<ApprovalDto, PolicyCancel>().ReverseMap();
            CreateMap<CityDto, City>().ReverseMap();
            CreateMap<Nominee,NomineeDto>() .ReverseMap();
            CreateMap<User, UserLogInResponseDto>()
                .ForMember(dest=>dest.RoleName,val=>val.MapFrom(src=>src.Role.RoleName));
            CreateMap<Agent, AgentResponseDto>().ReverseMap();
            CreateMap<AgentInputDto, AgentResponseDto>().ReverseMap();
            CreateMap<UserDto,UserUpdateDto>().ReverseMap();
            CreateMap<PolicyAccountDocument, PolicyAccountDocumentDto>().ReverseMap();
            CreateMap<Document, DocumentResponseDto>().ReverseMap();
            CreateMap<Customer, CustomerProfileDto>()
                .ForMember(dest => dest.City, val => val.MapFrom(src => src.City.CityName))
                .ForMember(dest=> dest.State,val=>val.MapFrom(src=>src.State.StateName));
            CreateMap<PolicyAccount, PolicyAccountResponseDto>()
                .ForMember(dest=>dest.PolicyName,val=>val.MapFrom(src=>src.Policy.Name));
            CreateMap<PolicyInstallment, PolicyInstallmentResponsDto>().ReverseMap();
        }
    }
}

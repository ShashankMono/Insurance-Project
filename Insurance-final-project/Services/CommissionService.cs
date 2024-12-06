using AutoMapper;
using Insurance_final_project.Constant;
using Insurance_final_project.Dto;
using Insurance_final_project.Exceptions;
using Insurance_final_project.Models;
using Insurance_final_project.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Insurance_final_project.Services
{
    public class CommissionService:ICommissionService
    {
        private readonly IRepository<Commission> _CommissionRepo;
        private readonly IMapper _Mapper;
        private readonly IRepository<PolicyAccount> _PolicyAccountRepo;
        private readonly IRepository<Agent> _agentRepo;
        public CommissionService(IRepository<Commission> repo,IRepository<Agent> agentRepo, IMapper mapper, IRepository<PolicyAccount> policyAccount)
        {
            _CommissionRepo = repo;
            _Mapper = mapper;
            _PolicyAccountRepo = policyAccount;
            _agentRepo = agentRepo;
        }
        public async Task<List<CommissionDto>> GetCommissions()
        {
            return _Mapper.Map<List<Commission>, List<CommissionDto>>(_CommissionRepo.GetAll().ToList());
        }

        public async Task<List<CommissionDto>> GetCommissionByAgentId(Guid agentId)
        {
            return _Mapper.Map<List<CommissionDto>>(_CommissionRepo.GetAll().Where(c => c.AgentId == agentId).ToList());
        }

        public async Task<Guid> AddCommission(CommissionDto commissionDto,double amountPaid)
        {
            var commission = _Mapper.Map<Commission>(commissionDto);
            if (_PolicyAccountRepo.Get(commission.PolicyAccountId) == null)
            {
                throw new InvalidGuidException("Account not exist!");
            }
            else if (_agentRepo.Get(commission.AgentId)==null)
            {
                throw new InvalidGuidException("Agent not found!");
            }
            var commissionPercentage = _PolicyAccountRepo.GetAll()
                                                .Include(pa => pa.Policy)
                                                .FirstOrDefault(pa => pa.Id == commission.PolicyAccountId)
                                                .Policy
                                                .CommissionPercentage;
            var commissionAmount = amountPaid * commissionPercentage;   
            commission.Amount = commissionAmount;
            
            //updating agent 
            var agent = _agentRepo.GetAll().AsNoTracking().FirstOrDefault(a=>a.AgentId == commission.AgentId);
            agent.CommissionEarned = agent.CommissionEarned + commissionAmount;
            agent.TotalCommission += commissionAmount;
            _agentRepo.Update(agent);
            return _CommissionRepo.Add(commission).CommissionId;
        }


    }
}


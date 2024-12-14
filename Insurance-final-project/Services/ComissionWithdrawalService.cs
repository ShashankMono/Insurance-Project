using AutoMapper;
using Insurance_final_project.Dto;
using Insurance_final_project.Exceptions;
using Insurance_final_project.Models;
using Insurance_final_project.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Insurance_final_project.Services
{
    public class ComissionWithdrawalService:ICommissionWithdrawalService
    {
        private readonly IRepository<CommissionWithdrawal> _CommissionWithdrawalRepo;
        private readonly IMapper _Mapper;
        private readonly IRepository<Agent> _agentRepo;
        private readonly IEmailService _emailService;
        public ComissionWithdrawalService(IRepository<CommissionWithdrawal> repo
            ,IRepository<Agent> agentRepo,
            IMapper mapper,
            IEmailService emailService)
        {
            _CommissionWithdrawalRepo = repo;
            _Mapper = mapper;
            _agentRepo = agentRepo;
            _emailService = emailService;
        }

        public async Task<List<CommissionWithdrawalDto>> GetCommissionsWithdrawal()
        {
            return _Mapper.Map<List<CommissionWithdrawal>, List<CommissionWithdrawalDto>>(_CommissionWithdrawalRepo.GetAll().ToList());
        }

        public async Task<Guid> AddWithdrawalRequest(CommissionWithdrawalDto withdrawRequest)
        {
            var agent = _agentRepo.GetAll().AsNoTracking().FirstOrDefault(a=>a.AgentId==withdrawRequest.AgentId);
            if (agent == null)
                throw new AgendNotFoundException("Agent not found.");
            if (agent.CommissionEarned < withdrawRequest.Amount)
                throw new InsufficientAmountException("Insufficient commission balance.");

            agent.CommissionEarned -= withdrawRequest.Amount;
            _agentRepo.Update(agent);
            _emailService.CommissionWithdrawalMail(withdrawRequest.AgentId,withdrawRequest.Amount);
            withdrawRequest.TransactionDate = DateTime.UtcNow;

            return _CommissionWithdrawalRepo.Add(_Mapper.Map<CommissionWithdrawal>(withdrawRequest)).Id;
        }

        public async Task<List<CommissionWithdrawalDto>> GetCommissionWithdrawalByAgentId(Guid agendId)
        {
            if(_agentRepo.Get(agendId) == null)
            {
                throw new AgendNotFoundException("Agent not found!");
            }
            return _Mapper.Map<List<CommissionWithdrawalDto>>(_CommissionWithdrawalRepo.GetAll().Where(c=>c.AgentId == agendId).ToList());
        }
    }
}

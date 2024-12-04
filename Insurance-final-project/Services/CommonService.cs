using Insurance_final_project.Constant;
using Insurance_final_project.Dto;
using Insurance_final_project.Models;
using Insurance_final_project.Repositories;
using Insurance_final_project.Mapper;
using AutoMapper;
namespace Insurance_final_project.Services
{
    public class CommonService : ICommonService
    {
        private readonly IRepository<Policy> _policyRepository;
        private readonly IRepository<State> _stateRepo;
        private readonly IRepository<City> _cityRepo;
        private readonly IMapper _mapper;

        public CommonService(IRepository<Policy> policyRepository,
            IRepository<State> stateRepo,
            IRepository<City> cityRepo,
            IMapper mapper
            )
        {
            _policyRepository = policyRepository;
            _stateRepo = stateRepo;
            _cityRepo = cityRepo;
            _mapper= mapper;
        }
        public List<string> GetapprovalTypes()
        {
            Array approvalTypes = Enum.GetValues(typeof(ApprovalType));
            List<string> types = new List<string>();
            foreach (ApprovalType status in approvalTypes) {
                types.Add(status.ToString());
            }
            return types;
        }

        public ICollection<CityDto> GetCities()
        {
            throw new NotImplementedException();
        }

        public List<PolicyDto> GetPolicies()
        {
            throw new NotImplementedException();
        }

        public List<string> GetPolicyAccountStatus()
        {
            throw new NotImplementedException();
        }

        public List<RoleDto> GetRoles()
        {
            throw new NotImplementedException();
        }

        public ICollection<StateDto> GetStates()
        {
            throw new NotImplementedException();
        }

        public List<string> GetTransactionStatus()
        {
            throw new NotImplementedException();
        }

        public List<string> GetVerificationType()
        {
            throw new NotImplementedException();
        }
    }
}

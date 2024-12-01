using Insurance_final_project.Constant;
using Insurance_final_project.Dto;
using Insurance_final_project.Models;
using Insurance_final_project.Repositories;

namespace Insurance_final_project.Services
{
    public class CommonService : ICommonService
    {
        private readonly IRepository<Policy> _policyRepository;
        private readonly IRepository<State> _stateRepo;
        private readonly IRepository<City> _cityRepo;

        public CommonService(IRepository<Policy> policyRepository,
            IRepository<State> stateRepo,
            IRepository<City> cityRepo
            )
        {
            _policyRepository = policyRepository;
            _stateRepo = stateRepo;
            _cityRepo = cityRepo;
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

        public List<CityDto> GetCities()
        {
            throw new NotImplementedException();
        }

        public List<PolicyDTO> GetPolicies()
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

        public List<StateDto> GetStates()
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

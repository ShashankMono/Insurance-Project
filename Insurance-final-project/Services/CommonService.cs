using AutoMapper;
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
        IMapper _mapper;
        private readonly IRepository<Role> _roleRepo;
        private readonly IRepository<PolicyType> _policyType;

        public CommonService(IRepository<Policy> policyRepository,
            IRepository<State> stateRepo,
            IRepository<City> cityRepo,
            IMapper mapper,
            IRepository<Role> roleRepo,
            IRepository<PolicyType> policyType
            )
        {
            _policyRepository = policyRepository;
            _stateRepo = stateRepo;
            _cityRepo = cityRepo;
            _mapper = mapper;
            _roleRepo = roleRepo;
            _policyType = policyType;
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


        public List<string> GetPolicyAccountStatus()
        {
            Array accountStatus = Enum.GetValues(typeof(PolicyAccountStatus));
            List<string> status = new List<string>();
            foreach (PolicyAccountStatus aStatus in accountStatus)
            {
                status.Add(aStatus.ToString());
            }
            return status;
        }

        public List<string> GetTransactionStatus()
        {
            Array TransactionType = Enum.GetValues(typeof(TransactionType));
            List<string> type = new List<string>();
            foreach (var status in TransactionType)
            {
                type.Add(status.ToString());
            }
            return type;
        }

        public List<string> GetVerificationType()
        {
            Array verification = Enum.GetValues(typeof(VerificationType));
            List<string> verify = new List<string>();
            foreach (var value in verification)
            {
                verify.Add(value.ToString());
            }
            return verify;
        }


        public List<string> GetpolicyInstallmentType()
        {
            Array installment = Enum.GetValues(typeof(InstallmentType));
            List<string> iType = new List<string>();
            foreach (var type in installment)
            {
                iType.Add(type.ToString());
            }
            return iType;
        }

        public List<string> GetDocumentType()
        {
            Array docs = Enum.GetValues(typeof(DocumentTypes));
            List<string> document = new List<string>();
            foreach (var type in docs)
            {
                document.Add(type.ToString());
            }
            return document;
        }
    }
}

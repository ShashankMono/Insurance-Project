using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface ICustomerService
    {
        public CustomerProfileDto GetCustomerById(Guid customerId);
        public CustomerProfileDto GetCustomerByUserId(Guid UserId);
        public bool UpdateProfile(CustomerDto customerDto);
        public CustomerDto RegisterCustomer(CustomerDto customerDto);
        public Task<List<CustomerProfileDto>> GetCustomerAccounts(string searchQuery);
        public bool ApproveCustomer(ApprovalDto approval);
    }

}

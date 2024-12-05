using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface ICustomerService
    {
        public CustomerDto GetCustomerById(Guid customerId);
        public bool UpdateProfile(CustomerDto customerDto);
        public CustomerDto RegisterCustomer(CustomerDto customerDto);
        public Task<List<CustomerDto>> GetCustomerAccounts();
    }

}

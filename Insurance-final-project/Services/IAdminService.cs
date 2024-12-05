using Insurance_final_project.Dto;
using Insurance_final_project.Models;

namespace Insurance_final_project.Services
{
    public interface IAdminService
    {
        public Task<Guid> AddAdmin(AdminDto adminDto);
        public Task<Guid> UpdateAdmin(AdminDto adminDto);
        public Task<AdminDto> GetAdmin(Guid id);

    }
}

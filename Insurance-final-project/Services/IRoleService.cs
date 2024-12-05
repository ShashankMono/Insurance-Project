using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface IRoleService
    {
        public Task<Guid> AddRole(RoleDto role);
        public Task<List<RoleDto>> GetRoles();
        public Task<Guid> UpdateRole(RoleDto roleDto);
    }
}

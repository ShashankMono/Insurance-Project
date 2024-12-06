using AutoMapper;
using Insurance_final_project.Dto;
using Insurance_final_project.Exceptions;
using Insurance_final_project.Models;
using Insurance_final_project.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Insurance_final_project.Services
{
    public class RoleService:IRoleService
    {
        private readonly IRepository<Role> _RoleRepo;
        private readonly IMapper _Mapper;
        public RoleService(IRepository<Role> repo, IMapper mapper)
        {
            _RoleRepo = repo;
            _Mapper = mapper;
        }

        public async Task<Guid> AddRole(RoleDto role)
        {
            check(role);
            var newRole = _Mapper.Map<RoleDto, Role>(role);
            Role roleAdded = _RoleRepo.Add(newRole);
            return roleAdded.RoleId;
        }
        public async Task<List<RoleDto>> GetRoles()
        {
            
            return _Mapper.Map<List<Role>, List<RoleDto>>(_RoleRepo.GetAll().ToList());
        }

        public void check(RoleDto role)
        {
            if (_RoleRepo.GetAll().FirstOrDefault(p => p.RoleName.ToLower() == role.RoleName.ToLower()) != null)
            {
                throw new DataAlreadyPresnetException("Role already exist!");
            }
        }

        public async Task<Guid> UpdateRole(RoleDto role)
        {
            check(role);
            if (_RoleRepo.GetAll().AsNoTracking().FirstOrDefault(r=>r.RoleId == role.RoleId) == null)
            {
                throw new InvalidGuidException("Invalid Role!");
            }
            return _RoleRepo.Update(_Mapper.Map<RoleDto, Role>(role)).RoleId;
        }
    }
}

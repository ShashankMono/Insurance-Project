using AutoMapper;
using Insurance_final_project.Constant;
using Insurance_final_project.Dto;
using Insurance_final_project.Exceptions;
using Insurance_final_project.Models;
using Insurance_final_project.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Insurance_final_project.Services
{
    public class AdminService : IAdminService
    {
        private readonly IRepository<Admin> _adminRepo;
        private readonly IMapper _mapper;
        public AdminService(IRepository<Admin> adminRepo,IMapper mapper)
        {
            _adminRepo = adminRepo;
            _mapper = mapper;
        }

        public async Task<Guid> AddAdmin(AdminDto adminDto)
        {
            return _adminRepo.Add(_mapper.Map<Admin>(adminDto)).AdminId;
        }

        public async Task<Guid> UpdateAdmin(AdminDto adminDto)
        {
            if(_adminRepo.GetAll().AsNoTracking().FirstOrDefault(a=>a.AdminId ==adminDto.AdminId) == null)
            {
                throw new InvalidGuidException("Admin not found!");
            }
            return _adminRepo.Update(_mapper.Map<Admin>(adminDto)).AdminId;
        }

        public async Task<AdminDto> GetAdmin(Guid id)
        {

            var admin =  _mapper.Map<AdminDto>(_adminRepo.Get(id));
            if (admin == null) {
                throw new AdminNotFoundException("Admin not found!");
            }
            return admin;
        }

    }
}

using AutoMapper;
using Insurance_final_project.Dto;
using Insurance_final_project.Exceptions;
using Insurance_final_project.Models;
using Insurance_final_project.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Insurance_final_project.Services
{
    public class CityService:ICityService
    {
        private readonly IRepository<City> _CityRepo;
        private readonly IMapper _Mapper;
        public CityService(IRepository<City> repo,IMapper mapper)
        {
            _CityRepo = repo;
            _Mapper = mapper;
        }
        public async Task<Guid> AddCity(CityDto city)
        {
            return _CityRepo.Add(_Mapper.Map<CityDto, City>(city)).CityId;
        }
        public async Task<Guid> UpdateCity(CityDto city)
        {
            if (_CityRepo.GetAll().AsNoTracking().FirstOrDefault(c=>c.CityId == city.CityId) == null)
            {
                throw new CityNotFoundException("City not found!");
            }
            return _CityRepo.Update(_Mapper.Map<CityDto, City>(city)).CityId;
        }
        public async Task<List<CityDto>> GetCities()
        {
            return _Mapper.Map<List<City>, List<CityDto>>(_CityRepo.GetAll().ToList());
        }

        public async Task<List<CityDto>> GetCitiesByStateId(Guid stateId)
        {
            return _Mapper.Map<List<City>, List<CityDto>>(_CityRepo.GetAll().Where(c=>c.StateId == stateId).ToList());
        }
    }
}

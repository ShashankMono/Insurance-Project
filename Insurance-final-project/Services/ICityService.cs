using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface ICityService
    {
        Task<Guid> UpdateCity(CityDto city);
        Task<Guid> AddCity(CityDto city);

        public Task<List<CityDto>> GetCities();
        public Task<List<CityDto>> GetCitiesByStateId(Guid stateId);
    }
}

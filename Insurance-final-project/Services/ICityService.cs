using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface ICityService
    {
        public Guid AddCity(CityDto city);
        public Guid UpdateCity(CityDto city);
        public int DeleteCity (Guid cityId);
        public List<CityDto> GetAllCity();
    }
}

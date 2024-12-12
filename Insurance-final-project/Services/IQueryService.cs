using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface IQueryService
    {
        public Task<Guid> SubmitQuery(QueryDto queryDto);
        public Task<Guid> ResponseToQuery(QueryDto queryDto);
        public Task<List<QueryDto>> GetQueryByCustomerId(Guid customerId);
        public Task<List<QueryDto>> GetAllQuery();
    }
}

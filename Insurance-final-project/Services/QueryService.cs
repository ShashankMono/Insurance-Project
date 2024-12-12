using AutoMapper;
using Insurance_final_project.Dto;
using Insurance_final_project.Exceptions;
using Insurance_final_project.Models;
using Insurance_final_project.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Insurance_final_project.Services
{
    public class QueryService:IQueryService
    {
        private readonly IRepository<Query> _QueryRepo;
        private readonly IMapper _Mapper;
        private readonly IRepository<Customer> _customerRepo;
        public QueryService(IRepository<Query> repo, IMapper mapper, IRepository<Customer> customerRepo)
        {
            _QueryRepo = repo;
            _Mapper = mapper;
            _customerRepo = customerRepo;
        }

        public async Task<List<QueryDto>> GetQueryByCustomerId(Guid customerId)
        {
            if(_customerRepo.Get(customerId) == null)
            {
                throw new CustomerNotFoundException("Customer not found!");
            }
            return _Mapper.Map<List<QueryDto>>(_QueryRepo.GetAll().Where(q=>q.CustomerId == customerId).ToList());
        }

        public async Task<List<QueryDto>> GetAllQuery()
        {
            return _Mapper.Map<List<QueryDto>>(_QueryRepo.GetAll().ToList());
        }

        public async Task<Guid> ResponseToQuery(QueryDto queryDto)
        {
            if (_QueryRepo.GetAll().AsNoTracking().FirstOrDefault(q=>q.QueryId == queryDto.QueryId) == null)
            {
                throw new InvalidGuidException("Invalid Query!");
            }
            return _QueryRepo.Update(_Mapper.Map<Query>(queryDto)).QueryId;
        }

        public async Task<Guid> SubmitQuery(QueryDto queryDto)
        {
            if(_customerRepo.Get(queryDto.CustomerId) == null)
            {
                throw new InvalidGuidException("Customer not found!");
            }
            var query = _Mapper.Map<Query>(queryDto);
            _QueryRepo.Add(query);
            return query.QueryId;
        }


    }
}

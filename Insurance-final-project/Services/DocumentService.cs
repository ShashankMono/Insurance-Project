using AutoMapper;
using Insurance_final_project.Dto;
using Insurance_final_project.Exceptions;
using Insurance_final_project.Models;
using Insurance_final_project.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Insurance_final_project.Services
{
    public class DocumentService:IDocumentService
    {
        private readonly IRepository<Document> _DocumentRepo;
        private readonly IMapper _Mapper;
        private readonly IRepository<Customer> _customerRepo;
        public DocumentService(IRepository<Document> repo, IMapper mapper, IRepository<Customer> customerRepo)
        {
            _DocumentRepo = repo;
            _Mapper = mapper;
            _customerRepo = customerRepo;
        }

        public async Task<Guid> AddDocument(DocumentDto document)
        {
            var existingDocument = _DocumentRepo.GetAll().AsNoTracking().FirstOrDefault(d => d.CustomerId == document.CustomerId && d.DocumentType == document.DocumentType);
            if(_customerRepo.Get(document.CustomerId) == null)
            {
                throw new InvalidGuidException("Customer not found!");
            }
            if(existingDocument != null)
            {
                throw new DocumentExistException("Document already exist!");
            }
            return _DocumentRepo.Add(_Mapper.Map<Document>(document)).DocumentId;
        }

        public async Task<Guid> ChangeApproveStatus(DocumentDto document)
        {
            if (_DocumentRepo.GetAll().AsNoTracking().FirstOrDefault(d=>d.DocumentId == document.DocumentId) == null)
            {
                throw new InvalidGuidException("Invalid Document!");
            }
            if (_customerRepo.Get(document.CustomerId) == null)
            {
                throw new InvalidGuidException("Customer not found!");
            }
            return _DocumentRepo.Update(_Mapper.Map<DocumentDto, Document>(document)).DocumentId;
        }

        public async Task<List<DocumentDto>> GetDocument()
        {
            return _Mapper.Map<List<DocumentDto>>(_DocumentRepo.GetAll().ToList());
        }
    }
}

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

        public async Task<Guid> ChangeApproveStatus(VerificationDto document)
        {
            var Document = _DocumentRepo.GetAll().AsNoTracking().FirstOrDefault(d => d.DocumentId == document.Id);
            if ( Document == null)
            {
                throw new InvalidGuidException("Invalid Document!");
            }
            if (_customerRepo.Get(document.CustomerId) == null)
            {
                throw new InvalidGuidException("Customer not found!");
            }
            Document.IsVerified = document.IsVerified;
            return _DocumentRepo.Update(Document).DocumentId;
        }

        public async Task<Guid> UpdateDocument(UpdateDocumentDto documentDto)
        {
            var document = _DocumentRepo.GetAll().AsNoTracking().FirstOrDefault(d => d.DocumentId == documentDto.DocumentId);
            if ( document== null)
            {
                throw new InvalidGuidException("Invalid Document!");
            }
            document.DocumentFileURL = documentDto.DocumentFileURL;
            return _DocumentRepo.Update(document).DocumentId;
        }

        public async Task<bool> DeleteDocument(Guid documentId)
        {
            var document = _DocumentRepo.GetAll().AsNoTracking().FirstOrDefault(d => d.DocumentId == documentId);
            if ( document== null)
            {
                throw new InvalidGuidException("Invalid Document!");
            }
            _DocumentRepo.Delete(document);
            return true;
        }

        public async Task<List<DocumentResponseDto>> GetDocument()
        {
            return _Mapper.Map<List<DocumentResponseDto>>(_DocumentRepo.GetAll().ToList());
        }
    }
}

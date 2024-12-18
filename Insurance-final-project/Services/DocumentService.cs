using AutoMapper;
using Insurance_final_project.Constant;
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
        private readonly IEmailService _emailService;
        public DocumentService(IRepository<Document> repo,IEmailService emailService, IMapper mapper, IRepository<Customer> customerRepo)
        {
            _DocumentRepo = repo;
            _Mapper = mapper;
            _customerRepo = customerRepo;
            _emailService= emailService;
        }

        public async Task<Guid> AddDocument(DocumentDto document)
        {
            var existingDocument = _DocumentRepo.GetAll().AsNoTracking().FirstOrDefault(d => d.CustomerId == document.CustomerId && d.DocumentType == document.DocumentType);
            var customer = _customerRepo.GetAll().AsNoTracking().FirstOrDefault(c => c.CustomerId == document.CustomerId);
            if (customer == null)
            {
                throw new InvalidGuidException("Customer not found!");
            }
            if(existingDocument != null)
            {
                throw new DocumentExistException("Document already exist!");
            }
            customer.IsApproved = ApprovalType.Pending.ToString();
            _customerRepo.Update(customer);
            return _DocumentRepo.Add(_Mapper.Map<Document>(document)).DocumentId;
        }

        public async Task<Guid> ChangeApproveStatus(VerificationDto document)
        {
            var Document = _DocumentRepo.GetAll().AsNoTracking().FirstOrDefault(d => d.DocumentId == document.Id);
            var customer = _customerRepo.GetAll().AsNoTracking().FirstOrDefault(c=>c.CustomerId == document.AccountId);
            if ( Document == null)
            {
                throw new InvalidGuidException("Invalid Document!");
            }
            if ( customer == null)
            {
                throw new InvalidGuidException("Customer not found!");
            }
            Document.IsVerified = document.IsVerified.ToLower() == "Verified".ToLower() || document.IsVerified == "Verify".ToLower()
                                                            ? VerificationType.Verified.ToString() : VerificationType.Rejected.ToString() ;
            if(Document.IsVerified == VerificationType.Rejected.ToString())
            {
                _emailService.RejectionMail(document.AccountId, document.Reason,//here account id is customer Id
                    $"{Document.DocumentName.ToUpper()} document rejected");
            }
            else if (Document.IsVerified == VerificationType.Verified.ToString())
            {
                var mail = customer.EmailId;
                var Subject = $"Status change of document uploaded on Kyc";
                var message = $"Document uploaded for kyc {Document.DocumentType} is Approved!";
                _emailService.ApprovalOrVrifiedMail(mail, Subject, message);
            }
            return _DocumentRepo.Update(Document).DocumentId;
        }

        public async Task<Guid> UpdateDocument(UpdateDocumentDto documentDto)
        {
            var document = _DocumentRepo.GetAll().AsNoTracking().FirstOrDefault(d => d.DocumentId == documentDto.DocumentId);
            var customer = _customerRepo.GetAll().AsNoTracking().FirstOrDefault(c=>c.CustomerId == document.CustomerId);
            if ( document== null)
            {
                throw new InvalidGuidException("Invalid Document!");
            }
            document.DocumentFileURL = documentDto.DocumentFileURL;
            document.IsVerified = VerificationType.Pending.ToString();
            customer.IsApproved = ApprovalType.Pending.ToString();
            _customerRepo.Update(customer);
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

        public async Task<List<DocumentResponseDto>> GetDocumentByCustomerId(Guid customerId)
        {
            var customer = _customerRepo.Get(customerId);
            var document = _DocumentRepo.GetAll().AsNoTracking().Where(d => d.CustomerId == customerId).ToList();
            if (customer == null)
            {
                throw new InvalidGuidException("Invalid cusotmer!");
            }
            
            return _Mapper.Map<List<DocumentResponseDto>>(document);
        }

        public async Task<List<DocumentResponseDto>> GetDocument()
        {
            return _Mapper.Map<List<DocumentResponseDto>>(_DocumentRepo.GetAll().ToList());
        }
    }
}

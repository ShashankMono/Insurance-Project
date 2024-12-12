using AutoMapper;
using Insurance_final_project.Constant;
using Insurance_final_project.Dto;
using Insurance_final_project.Exceptions;
using Insurance_final_project.Models;
using Insurance_final_project.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace Insurance_final_project.Services
{
    public class PolicyAccountDocumentService : IPolicyAccountDocumentService
    {
        private readonly IRepository<PolicyAccountDocument> _documentRepo;
        private readonly IMapper _Mapper;
        private readonly IRepository<PolicyAccount> _PolicyAccountRepository;
        private readonly IEmailService _emailService;
        public PolicyAccountDocumentService(IRepository<PolicyAccountDocument> documentRepo
            , IRepository<PolicyAccount> PolicyAccountRepository
            ,IMapper map
            ,IEmailService emailService) { 
            _documentRepo= documentRepo;
            _Mapper = map;
            _PolicyAccountRepository = PolicyAccountRepository;
            _emailService= emailService;
        }
        public async Task<Guid> AddDocument(PolicyAccountDocumentDto document)
        {
            check(document.PolicyAccountId);
            return _documentRepo.Add(_Mapper.Map<PolicyAccountDocument>(document)).DocumentId;
        }

        public async Task<bool> DeleteDocument(Guid guid)
        {
            var document = _documentRepo.GetAll().AsNoTracking().FirstOrDefault(d => d.DocumentId == guid);
            if (document == null)
            {
                throw new DocumentNotFoundException("Document not found!");
            }
            _documentRepo.Delete(document);
            return true;
        }
        public void check(Guid policyAccountId)
        {
            if(_PolicyAccountRepository.Get(policyAccountId) == null)
            {
                throw new InvalidGuidException("Account not found!");
            }
        }
        public async Task<List<PolicyAccountDocumentDto>> GetDocuments(Guid policyAccountId)
        {
            if(_PolicyAccountRepository.Get(policyAccountId) == null)
            {
                throw new InvalidGuidException("Invalid PolicyAccount");
            }

            return _Mapper.Map<List<PolicyAccountDocumentDto>>(_documentRepo.GetAll().AsNoTracking().Where(d=>d.PolicyAccountId == policyAccountId).ToList());
        }

        public async Task<Guid> UpdateDocument(UpdateDocumentDto updateDoc)
        {
            var document = _documentRepo.GetAll().AsNoTracking().FirstOrDefault(d => d.DocumentId == updateDoc.DocumentId);
            if (document == null)
            {
                throw new DocumentNotFoundException("Document not found!");
            }
            document.DocumentFileURL = updateDoc.DocumentFileURL;
            document.IsVerified = VerificationType.Pending.ToString();
            _documentRepo.Update(document);
            return document.DocumentId;
        }

        public async Task<List<PolicyAccountDocumentDto>> GetDocumentByAccountId(Guid AccountId)
        {
            var customer = _PolicyAccountRepository.Get(AccountId);
            var documents = _documentRepo.GetAll().AsNoTracking().Where(d => d.PolicyAccountId == AccountId).ToList();
            if (customer == null)
            {
                throw new InvalidGuidException("Invalid Account!");
            }

            return _Mapper.Map<List<PolicyAccountDocumentDto>>(documents);
        }
        public async Task<Guid> ChangeApproveStatus(VerificationDto document)
        {
            var Document = _documentRepo.GetAll().AsNoTracking().FirstOrDefault(d => d.DocumentId == document.Id);
            var policyAccount = _PolicyAccountRepository.GetAll().AsNoTracking().Include(pa=>pa.Policy).FirstOrDefault(pa=>pa.Id == document.AccountId);//AccountId hold policyAccountId
            if (Document == null)
            {
                throw new InvalidGuidException("Invalid Document!");
            }
            if ( policyAccount == null)
            {
                throw new InvalidGuidException("Account not found!");
            }
            Document.IsVerified = document.IsVerified.ToLower() == "Verified".ToLower() || document.IsVerified == "Verify".ToLower()
                                                            ? ApprovalType.Approved.ToString() : ApprovalType.Rejected.ToString(); ;
            
            if (Document.IsVerified == VerificationType.Rejected.ToString())
            {
                _emailService.RejectionMail(policyAccount.CustomerId, document.Reason,
                    $"{Document.DocumentName.ToUpper()} document rejected on policy scheme {policyAccount.Policy.Name} with investment {policyAccount.InvestmentAmount}");
            }
            return _documentRepo.Update(Document).DocumentId;
        }
    }
}

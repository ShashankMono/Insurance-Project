using AutoMapper;
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
        public PolicyAccountDocumentService(IRepository<PolicyAccountDocument> documentRepo
            , IRepository<PolicyAccount> PolicyAccountRepository
            ,IMapper map) { 
            _documentRepo= documentRepo;
            _Mapper = map;
            _PolicyAccountRepository = PolicyAccountRepository;
        }
        public async Task<Guid> AddDocument(PolicyAccountDocumentDto document)
        {
            check(document.DocumentId);
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
        public void check(Guid document)
        {
            if(_PolicyAccountRepository.Get(document) == null)
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
            check(updateDoc.DocumentId);
            var document = _documentRepo.GetAll().AsNoTracking().FirstOrDefault(d => d.DocumentId == updateDoc.DocumentId);
            if (document == null)
            {
                throw new DocumentNotFoundException("Document not found!");
            }
            document.DocumentFileURL = updateDoc.DocumentFileURL;
            _documentRepo.Update(document);
            return document.DocumentId;
        }
    }
}

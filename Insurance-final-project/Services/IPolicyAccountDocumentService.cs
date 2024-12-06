using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface IPolicyAccountDocumentService
    {
        public Task<Guid> AddDocument(PolicyAccountDocumentDto document);
        public Task<bool> DeleteDocument(Guid guid);
        public Task<List<PolicyAccountDocumentDto>> GetDocuments(Guid PolicyAccountId);
        public Task<Guid> UpdateDocument(UpdateDocumentDto updateDoc);
    }
}

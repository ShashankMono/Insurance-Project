using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface IDocumentService
    {
        public Task<Guid> ChangeApproveStatus(VerificationDto document);
        public Task<List<DocumentResponseDto>> GetDocument();
        public Task<Guid> AddDocument(DocumentDto document);
        public Task<Guid> UpdateDocument(DocumentDto document);
    }
}

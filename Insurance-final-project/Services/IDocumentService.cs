using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface IDocumentService
    {
        public Task<Guid> ChangeApproveStatus(DocumentDto document);
        public Task<List<DocumentDto>> GetDocument();
        public Task<Guid> AddDocument(DocumentDto document);
    }
}

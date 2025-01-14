using LMS.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IDocumentService
    {
        Task<IEnumerable<DocumentDto>> GetAllDocumentsAsync();
        Task<DocumentDto> GetDocumentByIdAsync(int id);
        Task CreateDocumentAsync(DocumentDto documentDto);
        Task<bool> UpdateDocumentAsync(int id, DocumentDto documentDto);
        Task<bool> DeleteDocumentAsync(int id);
    }
}

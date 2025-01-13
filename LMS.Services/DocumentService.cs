using AutoMapper;
using Domain.Models.Entities;
using LMS.Infrastructure.Data;
using LMS.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly LmsContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<DocumentService> _logger;

        public DocumentService(LmsContext context, IMapper mapper, ILogger<DocumentService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<DocumentDto>> GetAllDocumentsAsync()
        {
            var documents = await _context.Documents.ToListAsync();
            return _mapper.Map<IEnumerable<DocumentDto>>(documents);
        }

        public async Task<DocumentDto> GetDocumentByIdAsync(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document == null)
            {
                _logger.LogWarning("Document with ID {DocumentId} not found", id);
                throw new KeyNotFoundException($"Document with ID {id} not found");
            }
            return _mapper.Map<DocumentDto>(document);
        }

        public async Task CreateDocumentAsync(DocumentDto documentDto)
        {
            var document = _mapper.Map<Document>(documentDto);
            await _context.Documents.AddAsync(document);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Created new document with ID {DocumentId}", document.Id);
        }

        public async Task<bool> UpdateDocumentAsync(int id, DocumentDto documentDto)
        {
            var existingDocument = await _context.Documents.FindAsync(id);
            if (existingDocument == null)
            {
                _logger.LogWarning("Document with ID {DocumentId} not found for update", id);
                return false;
            }

            _mapper.Map(documentDto, existingDocument);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Updated document with ID {DocumentId}", id);

            return true;
        }

        public async Task<bool> DeleteDocumentAsync(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document == null)
            {
                _logger.LogWarning("Document with ID {DocumentId} not found for deletion", id);
                return false;
            }

            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Deleted document with ID {DocumentId}", id);

            return true;
        }
    }
}

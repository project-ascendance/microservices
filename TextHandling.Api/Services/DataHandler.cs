using Ascendance.Models;
using Microsoft.EntityFrameworkCore;
using TextHandling.Api.Data;
using TextHandling.Api.Services.Contracts;

namespace TextHandling.Api.Services
{
    internal class DataHandler: IDataHandler
    {
        private readonly TextHandlingDbContext _context;

        public DataHandler(TextHandlingDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task SaveTextContentAsync(TextContent content)
        {
            _context.Add(content);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTextContentAsync(TextContent content)
        {
            _context.Update(content); 
            await _context.SaveChangesAsync();
        }

        public async Task<TextContent> GetTextContentAsync(int id)
        {
            return await _context.TextContents.FirstAsync(x => x.Id == id);
        }

        public async Task RemoveTextContentAsync(int id)
        {
            _context.Remove(id);
            await _context.SaveChangesAsync();
        }
    }
}

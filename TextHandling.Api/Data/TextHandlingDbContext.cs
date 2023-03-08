using Ascendance.Models;
using Microsoft.EntityFrameworkCore;

namespace TextHandling.Api.Data
{
    public class TextHandlingDbContext: DbContext
    {
        public TextHandlingDbContext(DbContextOptions<TextHandlingDbContext> options) : base(options)
        {
        }

        public DbSet<TextContent> TextContents { get; set; }
    }
}

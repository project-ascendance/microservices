﻿using Ascendance.Models;
using MessageHandlers.Contracts;
using Microsoft.EntityFrameworkCore;
using TextHandling.Micro.Data;
using TextHandling.Micro.Services.Contracts;

namespace TextHandling.Micro.Services
{
    internal class DataHandler: IDataHandler
    {
        private readonly TextHandlingDbContext _context;
        private readonly IMQPublisher _mQPublisher;

        public DataHandler(TextHandlingDbContext dbContext, IMQPublisher mQPublisher)
        {
            _context = dbContext;
            _mQPublisher = mQPublisher;
        }

        public async Task SaveTextContentAsync(TextContent content)
        {
            _context.Add(content);
            await _context.SaveChangesAsync();
            _mQPublisher.AddExchange("testExchange");
            await _mQPublisher.PublishAsync("testExchange", content.ToString());
        }

        public async Task UpdateTextContentAsync(TextContent content)
        {
            _context.Update(content); 
            await _context.SaveChangesAsync();
        }

        public async Task<TextContent> GetTextContentAsync(int id)
        {
            return await _context.TextContents.SingleAsync(x => x.Id == id);
        }

        public async Task RemoveTextContentAsync(int id)
        {
            _context.Remove(id);
            await _context.SaveChangesAsync();
        }
    }
}

﻿using Ascendance.Models;

namespace TextHandling.Micro.Services.Contracts
{
    public interface IDataHandler
    {
        public Task SaveTextContentAsync(TextContent content);
        public Task UpdateTextContentAsync(TextContent content);
        public Task<TextContent> GetTextContentAsync(int id);
        public Task RemoveTextContentAsync(int id);
    }
}

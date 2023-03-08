using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageHandlerAPIs.Contracts
{
    public interface IMQPublisher
    {
        IMQPublisher AddExchange(string exchange);
        Task PublishAsync(string exchange, string JsonBody);
        void Dispose();
    }
}

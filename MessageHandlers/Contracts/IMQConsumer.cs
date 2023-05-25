using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageHandlerAPIs.Contracts
{
    public interface IMQConsumer
    {
        public event Action<string> MessageReceived;
        IMQConsumer AddExchange(string exchange);
        IMQConsumer AddQueue(string queue);
        IMQConsumer QueueBind(string queue, string exchange);
        Task Consume(string queue);
        void Dispose();
    }
}

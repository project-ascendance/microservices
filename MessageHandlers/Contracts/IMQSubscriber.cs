using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageHandlers.Contracts
{
    public interface IMQSubscriber
    {
        IMQSubscriber AddExchange(string exchange);
        IMQSubscriber AddQueue(string queue);
        IMQSubscriber QueueSubscribe(string queue, string exchange);
        Task ConsumeAsync(string queue);
        void Dispose();
    }
}

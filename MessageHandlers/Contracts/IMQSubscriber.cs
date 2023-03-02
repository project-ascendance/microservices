using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageHandlers.Contracts
{
    public interface IMQSubscriber
    {
        IMQSubscriber AddQueue(string queueName);
        IMQSubscriber QueueSubscribe(string queueName);
    }
}

using MessageHandlers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageHandlers
{
    public class MQSubscriber : IMQSubscriber
    {
        
        // TODO
        public MQSubscriber()
        {
            
        }
        
        public IMQSubscriber AddQueue(string queueName)
        {
            throw new NotImplementedException();
        }

        public IMQSubscriber QueueSubscribe(string queueName)
        {
            throw new NotImplementedException();
        }
    }
}

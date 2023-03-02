using MessageHandlers.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageHandlers
{
    public class MQPublisher : IMQPublisher
    {
        //TODO
        public MQPublisher()
        {
            
        }

        public IMQPublisher AddQueue(string queueName)
        {
            throw new NotImplementedException();
        }

        public Task PublishAsync(string exchange, string queueName, string JsonBody)
        {
            
            throw new NotImplementedException();
        }
    }
}

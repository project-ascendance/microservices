﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageHandlers.Contracts
{
    public interface IMQPublisher
    {
        IMQPublisher AddQueue(string queueName);
        Task PublishAsync(string exchange, string queueName, string JsonBody);
    }
}

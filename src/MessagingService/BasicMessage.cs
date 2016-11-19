using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RawRabbit.Attributes;
using RawRabbit.Configuration.Exchange;

namespace MessagingService
{
    [Queue(Name = "order-created", MessageTtl = 300, Durable = true, AutoDelete= false)]
    [Exchange(Name = "messagingservice", Type = ExchangeType.Topic)]
    [Routing(RoutingKey = "created", NoAck = true, PrefetchCount = 50)]
    public class BasicMessage
    {
        public string Text { get; set; }
    }
}

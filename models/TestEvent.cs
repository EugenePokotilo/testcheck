using System;
using NServiceBus;

namespace models
{
   public class TestEvent : IMessage {
        public string Query {get;set;}
        public string Details {get;set;}
     }
}

using System;
using System.Threading.Tasks;
using models;
using NServiceBus;

namespace consumer
{
    public class TestEventHandler : IHandleMessages<TestEvent>
    {
        public Task Handle(TestEvent message, IMessageHandlerContext context)
        {
            // Do something with the message here
            return Task.CompletedTask;
        }
    }
}
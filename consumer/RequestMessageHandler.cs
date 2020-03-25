using System;
using System.Threading.Tasks;
using models;
using Rebus.Bus;
using Rebus.Handlers;

namespace consumer
{
    public class RequestMessageHandler : IHandleMessages<RequestMessage>
    {
        private readonly IBus bus;

        private static int Scounter = 1;
        private  int counter = 1;

        public RequestMessageHandler(IBus bus)
        {
            this.bus = bus;
        }

        public async Task Handle(RequestMessage message)
        {
            // Do something with the message here

            //Console.WriteLine("---------------------------------");
            //Console.WriteLine($"Message received. details: {message.Details}");
            //Console.WriteLine("---------------------------------");

            //send response
            await this.bus.Advanced.Routing.Send("Server", new ResponseMessage() { Result =$"result_{Scounter++}_{counter++}"});
        }
    }
}
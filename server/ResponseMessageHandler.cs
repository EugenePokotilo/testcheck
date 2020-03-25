using System;
using System.Threading.Tasks;
using models;
using Rebus.Handlers;

namespace server
{
    public class ResponseMessageHandler : IHandleMessages<ResponseMessage>
    {
        public async Task Handle(ResponseMessage message)
        {

            //Console.WriteLine("---------------------------------");
            //Console.WriteLine($"Message received. result: {message.Result}");
            //Console.WriteLine("---------------------------------");


        }
    }
}
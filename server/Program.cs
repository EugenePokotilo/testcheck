using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using models;
using Rebus.Activation;
using Rebus.Bus;
using Rebus.Config;
using Rebus.Logging;
using Rebus.Routing.TypeBased;
using Rebus.SqlServer.Transport;
using Rebus.ServiceProvider;


namespace server
{
    class Program
    {
        static void Main(string[] args)
        {
           MainAsync().GetAwaiter().GetResult();
        }


        private static async Task MainAsync()
        {
           
            //var connection = @"Data Source=h2800713.stratoserver.net,47269;Database=NSB_ypokotylo;User Id=SuperAdmin;Password=Kddhealth2018?;Max Pool Size=100";
            var connection = @"Data Source=.;Database=NSB;Integrated Security=True;Max Pool Size=100";

            var services = new ServiceCollection();

            services.AutoRegisterHandlersFromAssemblyOf<ResponseMessageHandler>();


            services.AddRebus(configure => configure
                .Logging(l => l.ColoredConsole())
                .Transport(t => t.UseSqlServer(new SqlServerLeaseTransportOptions(connection), EndpointQueues.Server))
                );//.Routing(r => r.TypeBased().MapAssemblyOf<RequestMessage>("Consumer")));
            
            var provider = services.BuildServiceProvider();
            provider.UseRebus();


            var bus = provider.GetService<IBus>();

            for(var i=0; i < 20; i++)
            {
                var request = new RequestMessage() { Query = "query", Details = $"{i}" };
                await bus.Advanced.Routing.Send(EndpointQueues.Consumer, request);
            }

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }
    }
}

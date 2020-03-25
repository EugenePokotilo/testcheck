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

namespace consumer
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

            // Automatically register all handlers from the assembly of a given type...
            services.AutoRegisterHandlersFromAssemblyOf<RequestMessageHandler>();

            services.AddRebus(configure => configure
                .Logging(l => l.ColoredConsole())
                .Transport(t => t.UseSqlServer(new SqlServerTransportOptions(connection), EndpointQueues.Consumer))
                //.Routing(r => r.TypeBased().MapAssemblyOf<ResponseMessage>("Server"))
                 .Options(o =>
                 {
                     o.SetNumberOfWorkers(1);
                     o.SetMaxParallelism(1);
                 }));

            var provider = services.BuildServiceProvider();
            provider.UseRebus();




            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();

        }
    }
}

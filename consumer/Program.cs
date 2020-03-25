using System;
using System.Threading.Tasks;
using models;
using NServiceBus;

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
            var t = new TestEvent();

            var endpointConfiguration = new EndpointConfiguration("Consumer");

            var transport = endpointConfiguration.UseTransport<SqlServerTransport>();
            //var connection = @"Data Source=.;Database=NSB;Integrated Security=True;Max Pool Size=100";
            var connection = @"Data Source=h2800713.stratoserver.net,47269;Database=NSB_ypokotylo;User Id=SuperAdmin;Password=Kddhealth2018?;Max Pool Size=100";
            transport.ConnectionString(connection);
            transport.DefaultSchema("nsb");
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.AuditProcessedMessagesTo("audit");
            endpointConfiguration.EnableInstallers();


            var endpointInstance = await Endpoint.Start(endpointConfiguration)
              .ConfigureAwait(false);



            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();

            await endpointInstance.Stop().ConfigureAwait(false);
        }
    }
}

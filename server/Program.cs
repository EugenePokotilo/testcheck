using System;
using models;
using NServiceBus;
using System.Threading.Tasks;
namespace server
{
    class Program
    {
        static void Main(string[] args)
        {
           MainAsync().GetAwaiter().GetResult();
        }


        private static async Task MainAsync(){
 var endpointConfiguration = new EndpointConfiguration("Server");
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.AuditProcessedMessagesTo("audit");

            var transport = endpointConfiguration.UseTransport<SqlServerTransport>();
            //var connection = @"Data Source=.;Database=NSB;Integrated Security=True;Max Pool Size=100";
            var connection = @"Data Source=h2800713.stratoserver.net,47269;Database=NSB_ypokotylo;User Id=SuperAdmin;Password=Kddhealth2018?;Max Pool Size=100";
            transport.ConnectionString(connection);
            transport.DefaultSchema("nsb");

            // transport.DefaultSchema("sender");
            //transport.UseSchemaForQueue("error", "dbo");
            //transport.UseSchemaForQueue("audit", "dbo");
            //endpointConfiguration.UsePersistence<InMemoryPersistance>();

            var subscriptions = transport.SubscriptionSettings();
            subscriptions.SubscriptionTableName(
                tableName: "Subscriptions", 
                schemaName: "dbo");

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            var te = new TestEvent() { Query = "query", Details = "details" };
            await endpointInstance.Send("Consumer", te);

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();

            await endpointInstance.Stop()
    .ConfigureAwait(false);

        }
    }
}

using System;
using Confluent.Kafka;
using Confluent.Kafka.Admin;

namespace Learning.Kafka.TestClient.ConsoleHost.Broker
{
    internal static class ManageProcessor
    {
        private static TimeSpan _requestTimeout = TimeSpan.FromSeconds(3);

        public static void Execute(ManageOptions options)
        {
            switch (options.Scope)
            {
                case ManageScope.Broker:
                    ExecuteBroker(options);
                    break;

                case ManageScope.Topic:
                    ExecuteTopic(options);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(options.Scope), options.Scope, "Unknown options scope.");
            }
        }

        private static void ExecuteBroker(ManageOptions options)
        {
            switch (options.Action)
            {
                case ManageAction.List:
                    ExecuteBrokerList(options);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(options.Action), options.Action, "Unknown options action.");
            }
        }

        private static void ExecuteBrokerList(ManageOptions options)
        {
            using (var client = new AdminClientBuilder(new AdminClientConfig() { BootstrapServers = options.BootstrapServer }).Build())
            {
                var metadata = client.GetMetadata(_requestTimeout);

                Console.WriteLine("Originating Broker Information");
                Console.WriteLine();
                Console.WriteLine($"Broker ID:          {metadata.OriginatingBrokerId}");
                Console.WriteLine($"Broker Name:        {metadata.OriginatingBrokerName}");
                Console.WriteLine($"Local Topics:       {metadata.Topics.Count}");
                Console.WriteLine($"Brokers (Cluster):  {metadata.Brokers.Count}");
                Console.WriteLine();

                Console.WriteLine("Broker List");
                Console.WriteLine();
                Console.WriteLine("ID                   Host                 Port");
                Console.WriteLine("-------------------- -------------------- ----------");

                foreach (var broker in metadata.Brokers)
                {
                    Console.WriteLine($"{broker.BrokerId,-20} {broker.Host,-20} {broker.Port}");
                }

                Console.WriteLine();
            }
        }

        private static void ExecuteTopic(ManageOptions options)
        {
            switch (options.Action)
            {
                default:
                    throw new ArgumentOutOfRangeException(nameof(options.Action), options.Action, "Unknown options action.");
            }
        }
    }
}

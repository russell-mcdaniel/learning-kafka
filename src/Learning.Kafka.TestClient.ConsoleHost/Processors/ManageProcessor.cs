using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.Admin;

namespace Learning.Kafka.TestClient.ConsoleHost.Processors
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
                    throw new ArgumentOutOfRangeException(nameof(options.Scope), options.Scope, "Unknown or unsupported options scope.");
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
                    throw new ArgumentOutOfRangeException(nameof(options.Action), options.Action, "Unknown or unsupported options action.");
            }
        }

        private static void ExecuteBrokerList(ManageOptions options)
        {
            using (var client = GetAdminClient(options.BootstrapServer))
            {
                var metadata = client.GetMetadata(_requestTimeout);

                Console.WriteLine();
                Console.WriteLine("Originating Broker Information");
                Console.WriteLine();
                Console.WriteLine($"Broker ID:    {metadata.OriginatingBrokerId}");
                Console.WriteLine($"Broker Name:  {metadata.OriginatingBrokerName}");
                Console.WriteLine($"Brokers:      {metadata.Brokers.Count}");
                Console.WriteLine($"Topics:       {metadata.Topics.Count}");
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
                case ManageAction.List:
                    ExecuteTopicList(options);
                    break;

                case ManageAction.Create:
                    ExecuteTopicCreate(options);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(options.Action), options.Action, "Unknown or unsupported options action.");
            }
        }

        private static void ExecuteTopicCreate(ManageOptions options)
        {
            using (var client = GetAdminClient(options.BootstrapServer))
            {
                var topic = new TopicSpecification() { Name = options.Name, NumPartitions = options.Partitions, ReplicationFactor = options.ReplicationFactor };
                var topics = new List<TopicSpecification>() { topic };

                try
                {
                    var topicTask = client.CreateTopicsAsync(topics, new CreateTopicsOptions() { RequestTimeout = _requestTimeout, OperationTimeout = _requestTimeout }) as Task<List<CreateTopicReport>>;
                    var topicResults = topicTask.Result;

                    // TODO: Refactor reporting output to generalize it. Colorize based on individual report status.
                    foreach (var result in topicResults)
                    {
                        Console.WriteLine($"Topic '{result.Topic}' creation result: '{result.Error}'.");
                    }
                }
                catch (AggregateException ex) when (ex.InnerException is CreateTopicsException)
                {
                    var cte = ex.InnerException as CreateTopicsException;

                    foreach (var result in cte.Results)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Topic '{result.Topic}' creation result: '{result.Error}'.");

                        Console.ResetColor();
                    }
                }
            }
        }

        private static void ExecuteTopicList(ManageOptions options)
        {
            using (var client = GetAdminClient(options.BootstrapServer))
            {
                var metadata = client.GetMetadata(_requestTimeout);

                Console.WriteLine();
                Console.WriteLine("Originating Broker Information");
                Console.WriteLine();
                Console.WriteLine($"Broker ID:          {metadata.OriginatingBrokerId}");
                Console.WriteLine($"Broker Name:        {metadata.OriginatingBrokerName}");
                Console.WriteLine($"Local Topics:       {metadata.Topics.Count}");
                Console.WriteLine($"Brokers (Cluster):  {metadata.Brokers.Count}");
                Console.WriteLine();

                Console.WriteLine("Topic List");
                Console.WriteLine();
                Console.WriteLine("Topic                                    Partitions");
                Console.WriteLine("---------------------------------------- ----------");

                foreach (var topic in metadata.Topics)
                {
                    Console.WriteLine($"{topic.Topic,-40} {topic.Partitions.Count}");
                }

                Console.WriteLine();
            }
        }

        private static IAdminClient GetAdminClient(string bootstrapServer)
        {
            var config = new AdminClientConfig()
            {
                BootstrapServers = bootstrapServer,
            };

            var client = new AdminClientBuilder(config)
                .Build();

            return client;
        }
    }
}

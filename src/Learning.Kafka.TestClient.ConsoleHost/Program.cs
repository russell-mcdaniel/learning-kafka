using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.Admin;

namespace Learning.Kafka.TestClient.ConsoleHost
{
    class Program
    {
        private static string _server = "kafka:9092";   // "zookeeper:2181";
        private static string _topicName = "test-topic";

        static void Main(string[] args)
        {
            if (!TopicExists(_topicName))
                CreateTopic();

            ProduceMessage();
        }

        private static void CreateTopic()
        {
            var config = new AdminClientConfig() { BootstrapServers = _server };

            var topic = new TopicSpecification() { Name = _topicName };
            var topics = new List<TopicSpecification>() { topic };

            using (var client = new AdminClient(config))
            {
                var topicTask = client.CreateTopicsAsync(topics) as Task<List<CreateTopicExceptionResult>>;
                var topicResults = topicTask.Result;
            }
        }

        private static void ProduceMessage()
        {
            var prodConfig = new ProducerConfig() { BootstrapServers = _server };

            using (var producer = new Producer<byte[], byte[]>(prodConfig))
            {
                var messageText = "The quick brown fox jumps over the lazy dog.";
                var messageValue = Encoding.UTF8.GetBytes(messageText);
                var message = new Message<byte[], byte[]>() { Value = messageValue };

                var produceTask = producer.ProduceAsync(_topicName, message);
                var produceReport = produceTask.Result;
            }
        }

        private static bool TopicExists(string topicName)
        {
            var config = new AdminClientConfig() { BootstrapServers = _server };

            using (var client = new AdminClient(config))
            {
                // Which call auto-creates the topic?
                var metaBroker = client.GetMetadata(TimeSpan.FromSeconds(3));
                var metaTopic = client.GetMetadata(_topicName, TimeSpan.FromSeconds(3));    // Auto-creates topic. Broker setting?

                var resources = new List<ConfigResource>() { new ConfigResource() { Name = _topicName, Type = ResourceType.Topic } };
                var options = new DescribeConfigsOptions() { RequestTimeout = TimeSpan.FromSeconds(3) };

                var configTask = client.DescribeConfigsAsync(resources, options);
                var configResults = configTask.Result;
            }

            return false;
        }
    }
}

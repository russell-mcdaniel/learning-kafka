using System;
using CommandLine;

namespace Learning.Kafka.TestClient.ConsoleHost
{
    public enum ManageScope
    {
        Broker,
        Topic
    }

    public enum ManageAction
    {
        Configure,
        Create,
        Delete,
        Describe,
        List
    }

    public abstract class OptionsBase
    {
        /// <remarks>
        /// Kafka and the Confluent Kafka .NET API accept a collection of bootstrap
        /// servers. Supporting only a single broker for simplicity for now.
        /// </remarks>
        [Option('b', "bootstrap", Required = true, HelpText = "")]
        public string BootstrapServer { get; set; }
    }

    [Verb("manage", HelpText = "")]
    public class ManageOptions : OptionsBase
    {
        [Option('s', "scope", Required = true)]
        public ManageScope Scope { get; set; }

        [Option('a', "action", Required = true)]
        public ManageAction Action { get; set; }

        /// <summary>
        /// Specifies the name of the object to manage (e.g. broker or topic).
        /// </summary>
        [Option('n', "name")]
        public string Name { get; set; }
    }

    [Verb("produce", HelpText = "")]
    public class ProduceOptions : OptionsBase
    {
    }

    [Verb("consume", HelpText = "")]
    public class ConsumeOptions : OptionsBase
    {
    }
}

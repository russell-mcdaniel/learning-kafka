using System;
using Confluent.Kafka;
using Confluent.Kafka.Admin;

namespace Learning.Kafka.TestClient.ConsoleHost.Broker
{
    internal static class ManageProcessor
    {
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

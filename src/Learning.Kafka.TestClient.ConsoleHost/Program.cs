using System;
using CommandLine;
using Learning.Kafka.TestClient.ConsoleHost.Broker;

namespace Learning.Kafka.TestClient.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            // Configure parser with expected case-sensitivity behavior for the Windows environment.
            using (var parser = new Parser(settings => { settings.CaseInsensitiveEnumValues = true; }))
            {
                var result = parser.ParseArguments<ManageOptions, ProduceOptions, ConsumeOptions>(args)
                    .MapResult(
                        (ManageOptions options) => RunManage(options),
                        (ProduceOptions options) => RunProduce(options),
                        (ConsumeOptions options) => RunConsume(options),
                        _ => 1);
            }

            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("Press Enter to exit.");
                Console.ReadLine();
            }
        }

        static int RunManage(ManageOptions options)
        {
            return ManageProcessor.Execute(options);
        }

        static int RunProduce(ProduceOptions options)
        {
            return 0;
        }

        static int RunConsume(ConsumeOptions options)
        {
            return 0;
        }
    }
}

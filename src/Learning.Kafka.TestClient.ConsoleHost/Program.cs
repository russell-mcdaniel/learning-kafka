﻿using System;
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
                        (ManageOptions options) => Run(options),
                        (ProduceOptions options) => Run(options),
                        (ConsumeOptions options) => Run(options),
                        _ => 1);
            }

            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("Press Enter to exit.");
                Console.ReadLine();
            }
        }

        static int Run(OptionsBase options)
        {
            try
            {
                switch (options)
                {
                    case ManageOptions m:
                        ManageProcessor.Execute(m);
                        break;

                    case ProduceOptions p:
                        ProduceProcessor.Execute(p);
                        break;

                    case ConsumeOptions c:
                        ConsumeProcessor.Execute(c);
                        break;

                    case null:
                        throw new ArgumentNullException(nameof(options));

                    default:
                        throw new ArgumentOutOfRangeException(nameof(options), options, "Unknown options type.");
                }

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return 1;
            }
        }
    }
}

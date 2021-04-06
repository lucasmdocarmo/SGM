using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SGM.Events.Consumer
{
    public class CidadaoHandler : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = "test-consumer-group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };

            while (!cancellationToken.IsCancellationRequested)
            {
                using (var c = new ConsumerBuilder<Ignore, string>(config).Build())
                {
                    c.Subscribe("cidadao_topic");
                    var cts = new CancellationTokenSource();

                    try
                    {
                        while (true)
                        {
                            var message = c.Consume(cts.Token);
                            //
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        c.Close();
                    }
                }

                return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        }
    }
}

using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using SGM.Events.API.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SGM.Events.Consumer
{
    public class CidadaoHandler : IHostedService
    {
        public static HttpClient _Client;
        public CidadaoHandler()
        {
            _Client = new HttpClient();
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = "cidadao-topic-consumer",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };

            while (!cancellationToken.IsCancellationRequested)
            {
                using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
                consumer.Subscribe("cidadao-topic");
                var cts = new CancellationTokenSource();

                try
                {
                    while (true)
                    {
                        var message = consumer.Consume(cts.Token);
                        ReplicarCidadao(message.Value);
                    }
                }
                catch (OperationCanceledException)
                {
                    consumer.Close();
                }
            }
            return Task.CompletedTask;
        }
        private void ReplicarCidadao(string request)
        {
            var data = new StringContent(request, Encoding.UTF8, "application/json");
            _Client.PostAsync("https://localhost:44368/api/v1/Cidadao", data).ConfigureAwait(true);
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        }
    }
}

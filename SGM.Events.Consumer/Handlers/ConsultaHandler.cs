using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SGM.Events.Consumer.Handlers
{
    public class ConsultaHandler : IHostedService
    {
        public static HttpClient _Client;
        public ConsultaHandler()
        {
            _Client = new HttpClient();
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = "consulta-topic-consumer",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };

            while (!cancellationToken.IsCancellationRequested)
            {
                using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
                consumer.Subscribe("consulta-topic");
                var cts = new CancellationTokenSource();

                try
                {
                    while (true)
                    {
                        var message = consumer.Consume(cts.Token);
                        ReplicarConsulta(message.Value);
                    }
                }
                catch (OperationCanceledException)
                {
                    consumer.Close();
                }
            }
            return Task.CompletedTask;
        }
        private void ReplicarConsulta(string request)
        {
            var data = new StringContent(request, Encoding.UTF8, "application/json");
            _Client.PostAsync("https://localhost:44397/api/v1/Consulta", data).ConfigureAwait(true);
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        }
    }
}

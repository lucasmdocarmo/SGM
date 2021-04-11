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
    public class UsuarioHandler : IHostedService
    {
        public static HttpClient _Client;
        public UsuarioHandler()
        {
            _Client = new HttpClient();
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = "usuario-topic-consumer",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };

            while (!cancellationToken.IsCancellationRequested)
            {
                using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
                consumer.Subscribe("usuario-topic");
                var cts = new CancellationTokenSource();

                try
                {
                    while (true)
                    {
                        var message = consumer.Consume(cts.Token);
                        ReplicarUsuario(message.Value);
                    }
                }
                catch (OperationCanceledException)
                {
                    consumer.Close();
                }
            }
            return Task.CompletedTask;
        }
        private void ReplicarUsuario(string request)
        {
            var data = new StringContent(request, Encoding.UTF8, "application/json");
            _Client.PostAsync("https://localhost:44393/api/v1/Usuario", data).ConfigureAwait(true);
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        }
    }
}

using Confluent.Kafka;
using Newtonsoft.Json;
using SGM.Events.API.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGM.Events.API.Producers
{
    public class ServicesProducer : IServicesProducer
    {
        public async Task<bool> SendCidadaoByKafka(CidadaoWrapper payload)
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
            var message = JsonConvert.SerializeObject(payload);
            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    producer.ProduceAsync("cidadao-topic", new Message<Null, string> { Value = message }).GetAwaiter().GetResult();
                    producer.Flush(TimeSpan.FromSeconds(5));

                    return await Task.FromResult(true);
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }

            return await Task.FromResult(false);
        }
        public async Task<bool> SendUsuarioByKafka(UsuarioWrapper payload)
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
            var message = JsonConvert.SerializeObject(payload);
            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    producer.ProduceAsync("usuario-topic", new Message<Null, string> { Value = message }).GetAwaiter().GetResult();
                    producer.Flush(TimeSpan.FromSeconds(5));

                    return await Task.FromResult(true);
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }

            return await Task.FromResult(false);
        }
        public async Task<bool> SendConsultaByKafka(ConsultaWrapper payload)
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
            var message = JsonConvert.SerializeObject(payload);
            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    producer.ProduceAsync("consulta-topic", new Message<Null, string> { Value = message }).GetAwaiter().GetResult();
                    producer.Flush(TimeSpan.FromSeconds(5));

                    return await Task.FromResult(true);
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }

            return await Task.FromResult(false);
        }
        public async Task<bool> SendPacienteByKafka(PacienteWrapper payload)
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
            var message = JsonConvert.SerializeObject(payload);
            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    producer.ProduceAsync("paciente-topic", new Message<Null, string> { Value = message }).GetAwaiter().GetResult();
                    producer.Flush(TimeSpan.FromSeconds(5));

                    return await Task.FromResult(true);
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }

            return await Task.FromResult(false);
        }
    }
}

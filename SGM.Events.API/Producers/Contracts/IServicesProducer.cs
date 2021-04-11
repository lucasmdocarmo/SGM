using SGM.Events.API.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGM.Events.API
{
    public interface IServicesProducer
    {
        Task<bool> SendCidadaoByKafka(CidadaoWrapper payload);
        Task<bool> SendPacienteByKafka(PacienteWrapper payload);
        Task<bool> SendConsultaByKafka(ConsultaWrapper payload);
        Task<bool> SendUsuarioByKafka(UsuarioWrapper payload);
    }
}

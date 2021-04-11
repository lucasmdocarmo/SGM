using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGM.Events.API.Wrappers
{
    public class ConsultaWrapper
    {
        public string Especialidade { get; set; }
        public string Descricao { get; set; }
        public string InformacoesMedicas { get; set; }
        public DateTime DataConsulta { get; set; }
        public bool Confirmada { get; set; }
        public Guid PacienteId { get; set; }
        public Guid MedicoId { get; set; }
    }
}

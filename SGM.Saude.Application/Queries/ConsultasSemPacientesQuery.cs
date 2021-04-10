using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Saude.Application.Queries
{
    public class ConsultasSemPacientesQuery
    {
        public Guid ConsultaId { get; set; }
        public string Especialidade { get; set; }
        public DateTime? DataConsulta { get; set; }
        public string Descricaco { get; set; }
        public string Medico { get; set; }
        public string CRM { get; set; }
    }
}

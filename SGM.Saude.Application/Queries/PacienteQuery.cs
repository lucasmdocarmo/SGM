using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Saude.Application.Queries
{
    public class PacienteQuery
    {
        public PacienteQuery()
        {
            this.Consultas = new List<PacienteConsultas>();
        }
        public List<PacienteConsultas> Consultas { get; set; }
    }
    public class PacienteConsultas
    {
        public string Especialidade { get; set; }
        public string Descricao { get; set; }
        public string InformacoesMedicas { get; set; }
        public DateTime? DataConsulta { get; set; }
        public bool Confirmada { get; set; }
        public Guid? PacienteId { get; set; }
        public string Medico { get; set; }
        public string CRM { get; set; }
        public Guid MedicoId { get; set; }
    }
}

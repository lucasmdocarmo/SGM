using SGM.Shared.Core.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Cidadao.Application.Queries.Cidadao
{
    public class ConsultarConsultaMedicaQuery : IQuery
    {   
        public string CPF { get; set; }
        public string Token { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
    public class ConsultarConsultaMedicaQueryResponse
    {
        public List<PacienteConsultas> Consultas { get; set; }
    }
    public class PacienteConsultas
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

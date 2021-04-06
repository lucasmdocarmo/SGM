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
        public string Apikey { get; set; }
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
        public string Medico { get; set; }
        public string CRM { get; set; }
        public Guid MedicoId { get; set; }
    }
    public class MedicoQuery
    {
        public bool atendeTodosOsDias { get; set; }
        public string nome { get; set; }
        public string profissao { get; set; }
        public string crm { get; set; }
        public string especialidade { get; set; }
        public bool atendeSegunda { get; set; }
        public bool atendeTerca { get; set; }
        public bool atendeQuarta { get; set; }
        public bool atendeQuinta { get; set; }
        public bool atendeSexta { get; set; }
        public bool atendeSabado { get; set; }
        public bool atendeDomingo { get; set; }
        public bool ativo { get; set; }
        public string email { get; set; }
        public string telefone { get; set; }
        public double valorHora { get; set; }
        public string clinicaId { get; set; }
        public string id { get; set; }
    }
}
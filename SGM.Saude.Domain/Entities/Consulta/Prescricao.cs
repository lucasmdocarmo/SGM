using SGM.Shared.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Saude.Domain.Entities.Consulta
{
    public sealed class Prescricao :BaseEntity
    {
        internal Prescricao() { }
        public Prescricao(string detalhes, bool retornar, string resumo, DateTime dataInicial, DateTime validade, Guid consultaId)
        {
            Detalhes = detalhes;
            Retornar = retornar;
            Resumo = resumo;
            DataInicial = dataInicial;
            Validade = validade;
            ConsultaId = consultaId;
        }

        public string Detalhes { get; private set; }
        public bool Retornar { get; private set; }
        public string Resumo { get; private set; }
        public DateTime DataInicial { get; private set; }
        public DateTime Validade { get; private set; }

        public Consultas Consulta { get; private set; }
        public Guid ConsultaId { get; private set; }

        public void EditarPrescricao(string detalhes, bool retornar, string resumo, DateTime dataInicial, DateTime validade, Guid consultaId)
        {

            Detalhes = detalhes;
            Retornar = retornar;
            Resumo = resumo;
            DataInicial = dataInicial;
            Validade = validade;
            ConsultaId = consultaId;
        }
    }
}

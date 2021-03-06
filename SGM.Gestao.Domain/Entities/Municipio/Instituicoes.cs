using SGM.Gestao.Domain.Entities.Usuarios;
using SGM.Shared.Core.Entity;
using SGM.Shared.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Gestao.Domain.Entities
{
    public sealed class Instituicoes :BaseEntity
    {
        public string Nome { get; set; }
        public string RazaoSocial { get; set; }
        public string CNPJ { get; set; }
        public string Responsavel { get; set; }
        public string Email { get; set; }
        public ETipoInstituicao TipoInstituicao { get; set; }
        public IReadOnlyCollection<Funcionarios> Funcionarios { get; set; }
        public Guid FuncionariosId { get; set; }
        public Municipio Municipio { get; set; }
        public Guid MunicipioId { get; set; }

    }
}

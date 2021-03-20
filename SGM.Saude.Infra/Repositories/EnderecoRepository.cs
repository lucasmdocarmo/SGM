using SGM.Saude.Domain.Entities.Clinicas;
using SGM.Saude.Infra.Context;
using SGM.Shared.Core.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Saude.Infra.Repositories
{
    public class EnderecoRepository : BaseRepository<Endereco, SaudeContext>, Contracts.IEnderecoRepository
    {
        public EnderecoRepository(SaudeContext db) : base(db) { }
    }
}

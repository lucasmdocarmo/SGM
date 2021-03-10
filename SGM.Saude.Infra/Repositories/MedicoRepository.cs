using SGM.Saude.Domain.Entities;
using SGM.Saude.Infra.Context;
using SGM.Shared.Core.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Saude.Infra.Repositories
{
    public class MedicoRepository : BaseRepository<Medicos, SaudeContext>, Contracts.IMedicosRepository
    {
        protected MedicoRepository(SaudeContext db) : base(db) { }
    }
}

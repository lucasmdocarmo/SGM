using SGM.Saude.Domain.Entities;
using SGM.Saude.Infra.Context;
using SGM.Saude.Infra.Repositories.Contracts;
using SGM.Shared.Core.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Saude.Infra.Repositories
{
    public class ConsultaRepository : BaseRepository<Consultas, SaudeContext>, IConsultaRepository
    {
        protected ConsultaRepository(SaudeContext db) : base(db) { }

    }
}

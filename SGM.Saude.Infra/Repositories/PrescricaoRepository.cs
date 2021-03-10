using SGM.Saude.Domain.Entities.Consulta;
using SGM.Saude.Infra.Context;
using SGM.Shared.Core.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Saude.Infra.Repositories
{
    public class PrescricaoRepository : BaseRepository<Prescricao, SaudeContext>, Contracts.IPrescricaoRepository
    {
        protected PrescricaoRepository(SaudeContext db) : base(db) { }
    }
}

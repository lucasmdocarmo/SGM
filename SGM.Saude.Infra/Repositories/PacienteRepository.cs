using SGM.Saude.Domain.Entities;
using SGM.Saude.Infra.Context;
using SGM.Shared.Core.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Saude.Infra.Repositories
{
    public class PacienteRepository : BaseRepository<Paciente, SaudeContext>, Contracts.IPacienteRepository
    {
        protected PacienteRepository(SaudeContext db) : base(db) { }
    }
}

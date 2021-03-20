using SGM.Saude.Domain.Entities.Clinicas;
using SGM.Saude.Infra.Context;
using SGM.Saude.Infra.Repositories.Contracts;
using SGM.Shared.Core.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Saude.Infra.Repositories
{
    public class ClinicaRepository : BaseRepository<Clinica, SaudeContext>, IClinicaRepository
    {
        public ClinicaRepository(SaudeContext db) : base(db) { }
    }
}

using SGM.Saude.Domain.Entities;
using SGM.Saude.Infra.Context;
using SGM.Shared.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Saude.Infra.Repositories.Contracts
{
    public interface IPacienteRepository : IRepository<Paciente, SaudeContext>
    {
    }
}

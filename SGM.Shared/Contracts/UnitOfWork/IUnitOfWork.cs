using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SGM.Shared.Core.Contracts.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
        void Rollback();
        void Begin();
        bool CheckIfDatabaseExists();
    }
}

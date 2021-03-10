using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SGM.Shared.Core.Queries.Handler
{
    public interface IQueryHandler<T> where T : IQuery
    {
        ValueTask<IQueryResult> Handle(T command);
    }
}

using Flunt.Notifications;
using SGM.Cidadao.Application.Queries.Results.Cidadao;
using SGM.Shared.Core.Queries;
using SGM.Shared.Core.Queries.Handler;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SGM.Cidadao.Application.Queries
{
    public class CidadaoQueries : Notifiable, IQueryHandler<CidadaoQueryResult>
    {
        public ValueTask<IQueryResult> Handle(CidadaoQueryResult command)
        {
            throw new NotImplementedException();
        }
    }
}

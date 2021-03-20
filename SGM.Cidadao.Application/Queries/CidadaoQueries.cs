using Flunt.Notifications;
using SGM.Cidadao.Application.Queries.Cidadao;
using SGM.Cidadao.Application.Queries.Results.Cidadao;
using SGM.Cidadao.Infra.Repositories.Contracts;
using SGM.Shared.Core.Application;
using SGM.Shared.Core.Queries;
using SGM.Shared.Core.Queries.Handler;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SGM.Cidadao.Application.Queries
{
    public class CidadaoQueries : Notifiable, IQueryHandler<CidadaoQueryResult>, IQueryHandler<ConsultarConsultaMedicaQuery>
    {
        private readonly ICidadaoRepository _cidadaoRepository;
        public QueryResult _result;
        public CidadaoQueries(ICidadaoRepository cidadaoRepository)
        {
            _cidadaoRepository = cidadaoRepository;
            _result = new QueryResult();
        }

        public async ValueTask<IQueryResult> Handle(CidadaoQueryResult command)
        {
            var cidadaoEntity = await _cidadaoRepository.Search(x=>x.CPF.Numero == command.CPF).ConfigureAwait(true);

            if (cidadaoEntity == null)
            {
                AddNotification("Cidadao", "Cidadao Nao Encontrada.");
                return new QueryResult(false, base.Notifications);
            }

            _result.Result = cidadaoEntity;
            return new QueryResult(true, cidadaoEntity);
        }

        public ValueTask<IQueryResult> Handle(ConsultarConsultaMedicaQuery command)
        {
            throw new NotImplementedException();
        }
    }
}

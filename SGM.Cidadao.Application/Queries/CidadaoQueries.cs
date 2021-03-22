using Flunt.Notifications;
using Newtonsoft.Json;
using SGM.Cidadao.Application.Queries.Cidadao;
using SGM.Cidadao.Application.Queries.Results.Cidadao;
using SGM.Cidadao.Infra.Repositories.Contracts;
using SGM.Shared.Core.Application;
using SGM.Shared.Core.Queries;
using SGM.Shared.Core.Queries.Handler;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace SGM.Cidadao.Application.Queries
{
    public class CidadaoQueries : Notifiable, IQueryHandler<CidadaoQueryResult>, IQueryHandler<ConsultarConsultaMedicaQuery>
    {
        public static HttpClient _Client { get; set; }
        private readonly ICidadaoRepository _cidadaoRepository;
        public QueryResult _result;
        public CidadaoQueries(ICidadaoRepository cidadaoRepository)
        {
            _cidadaoRepository = cidadaoRepository;
            _result = new QueryResult();
            _Client = new HttpClient();
        }

        public async ValueTask<IQueryResult> Handle(CidadaoQueryResult command)
        {
            var cidadaoEntity = await _cidadaoRepository.Search(x=>x.CPF == command.CPF).ConfigureAwait(true);

            if (cidadaoEntity == null)
            {
                AddNotification("Cidadao", "Cidadao Nao Encontrada.");
                return new QueryResult(false, base.Notifications);
            }

            _result.Result = cidadaoEntity;
            return new QueryResult(true, cidadaoEntity);
        }

        public async ValueTask<IQueryResult> Handle(ConsultarConsultaMedicaQuery command)
        {
            _Client.DefaultRequestHeaders.Add("Authorization", command.Token);
            var result = await _Client.GetAsync("https://localhost:44397/api/v1/Consultas/Paciente/" + command.CPF);

            if (result.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<List<PacienteConsultas>>(result.Content.ReadAsStringAsync().Result);
                return new QueryResult(true, res);
            }
            return new QueryResult(false,"Paciente Não Encontrado.");
        }
    }
}

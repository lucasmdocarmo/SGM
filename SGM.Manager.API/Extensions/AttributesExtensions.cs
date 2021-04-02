using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SGM.Manager.API.Extensions
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class AttributesExtensions : Attribute, IAsyncActionFilter
    {
        private static HttpClient _client = new HttpClient();
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("ApiKey", out var extractedApiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "API KEY NÃO INFORMADA"
                };
                return;
            }

            var check = await CheckKeys(extractedApiKey);

            if (!check)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "API KEY INVALIDA PARA SISTEMA MANAGER"
                };
                return;
            }

            await next();
        }
        private async Task<bool> CheckKeys(string key)
        {
            var keys = await _client.GetAsync("https://localhost:44393/api/v1/Integracoes").ConfigureAwait(true);
            if (keys.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<List<IntegrationObject>>(keys.Content.ReadAsStringAsync().Result);

                foreach (var item in result)
                {
                    if (item.ApiKey != key && item.SistemaRaiz != ESistema.Manager)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }
    public class IntegrationObject
    {
        public string Sistema { get; set; }
        public ESistema SistemaRaiz { get; set; }
        public string ApiKey { get; set; }
        public string AppIntegrationCode { get; set; }
        public string Id { get; set; }
    }
    public enum ESistema
    {
        Cidadao = 1,
        Manager = 2,
        Saude = 3
    }
}

using SGM.Shared.Core.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Cidadao.Application.Queries.Results.Cidadao
{
    public class CidadaoQueryResult : IQuery
    {
        public string CPF { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
}

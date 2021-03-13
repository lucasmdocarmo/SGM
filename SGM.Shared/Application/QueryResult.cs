using Flunt.Notifications;
using SGM.Shared.Core.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Shared.Core.Application
{
    public class QueryResult : IQueryResult
    {
        public QueryResult(bool success, IReadOnlyCollection<Notification> messages)
        {
            Success = success;
            Messages = messages;
        }

        public QueryResult(bool success, dynamic result)
        {
            Success = success;
            Result = result;
        }

        public bool Success { get; set; }
        public dynamic Result { get; set; }
        public IReadOnlyCollection<Notification> Messages { get; set; }
    }
}

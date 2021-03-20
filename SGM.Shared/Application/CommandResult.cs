using Flunt.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGM.Shared.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Shared.Core.Application
{
    public class CommandResult : ICommandResult
    {
        public CommandResult() { }
        protected ActionResult ViewModelResult { get; set; }
        public bool Success { get; set; }
        public object Result { get; set; }
        public ActionResult ViewModel => ViewModelResult;
        public IReadOnlyCollection<Notification> Messages { get; set; }

        public CommandResult(bool success)
        {
            Success = success;
        }
        public CommandResult(bool success, IReadOnlyCollection<Notification> messages)
        {
            Success = success;
            Messages = messages;
        }
        public CommandResult(bool success, object result)
        {
            Result = result;
            Success = success;
        }
        public void ValidationErrors(IEnumerable<Notification> notifications)
        {
            ViewModelResult = new PreconditionFailedObjectResult(notifications);
        }
    }
    public class PreconditionFailedObjectResult : ObjectResult
    {
        private const int DefaultStatusCode = StatusCodes.Status412PreconditionFailed;

        public PreconditionFailedObjectResult(object value) : base(value)
        {
            StatusCode = DefaultStatusCode;
        }
    }
}

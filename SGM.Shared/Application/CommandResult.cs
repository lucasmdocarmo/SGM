using Flunt.Notifications;
using SGM.Shared.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Shared.Core.Application
{
    public class CommandResult : ICommandResult
    {
        public CommandResult() { }
        public bool Success { get; set; }
        public object Result { get; set; }
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
    }
}

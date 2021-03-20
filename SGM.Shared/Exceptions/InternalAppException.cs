using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Shared.Core.Exceptions
{
    public class InternalAppException : Exception
    {
        public override string Message { get; }
        public bool Error { get; }
        public InternalAppException(string message) : base(message)
        {
            this.Message = message;
        }
        public InternalAppException(string message, bool error) : base(message)
        {
            this.Message = message;
            this.Error = error;
        }
    }
}

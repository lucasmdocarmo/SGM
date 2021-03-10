using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Shared.Core.Commands
{
    public interface ICommand { bool Validate(); }
    public interface ICommandResult { }
}

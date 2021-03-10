using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SGM.Shared.Core.Commands.Handler
{
    public interface ICommandHandler<T> where T : ICommand
    {
        ValueTask<ICommandResult> Handle(T command);
    }
}

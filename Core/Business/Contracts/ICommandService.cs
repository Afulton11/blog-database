using System;
namespace DatabaseFactory.Data.Contracts
{
    public interface ICommandService<TCommand> where TCommand : ICommand
    {
        void Execute(TCommand command);
    }
}

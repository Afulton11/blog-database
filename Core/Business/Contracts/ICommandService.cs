using Core.Data.Commands;

namespace Core.Business.Contracts
{
    public interface ICommandService<TCommand> where TCommand : ICommand
    {
        void Execute(TCommand command);
    }
}

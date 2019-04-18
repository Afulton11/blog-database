using Domain.Data.Commands;

namespace Domain.Business.CommandServices
{
    public interface ICommandService<TCommand> where TCommand : ICommand
    {
        void Execute(TCommand command);
    }
}

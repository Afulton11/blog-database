using Domain.Data.Commands.Points;

namespace Domain.Business.CommandServices.Points
{
    public interface ICreatePointCommandService<TPointCommand> : ICommandService<TPointCommand>
        where TPointCommand : CreatePointCommand
    {
    }
}

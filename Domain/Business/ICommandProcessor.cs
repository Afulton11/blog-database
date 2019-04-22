using Domain.Data.Commands;
using System.Threading.Tasks;

namespace Domain.Business
{
    public interface ICommandProcessor
    {
        Task Execute(ICommand command);
    }
}

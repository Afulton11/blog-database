using Domain.Business.CommandServices;
using Domain.Data.Commands;

namespace DataAccess.CommandServices
{
    public class CreateFollowerCommandService : ICommandService<CreateFollowerCommand>
    {

        public void Execute(CreateFollowerCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}

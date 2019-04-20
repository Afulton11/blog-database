using Domain.Data.Commands;
using Domain.Data.Commands.Comments;

namespace Domain.Business.CommandServices.Comments
{
    public interface ICreateCommentCommandService : ICommandService<CreateCommentCommand>
    {
    }
}

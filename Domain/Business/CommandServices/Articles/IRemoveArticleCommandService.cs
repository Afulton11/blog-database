using Domain.Data.Commands.Articles;

namespace Domain.Business.CommandServices.Articles
{
    public interface IRemoveArticleCommandService : ICommandService<RemoveArticleCommand>
    {
    }
}

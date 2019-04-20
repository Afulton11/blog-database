using Domain.Data.Commands.Articles;

namespace Domain.Business.CommandServices.Articles
{
    public interface ICreateArticleCommandService : ICommandService<CreateArticleCommand>
    {
    }
}

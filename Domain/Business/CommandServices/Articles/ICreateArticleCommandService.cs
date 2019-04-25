using Domain.Data.Commands.Articles;

namespace Domain.Business.CommandServices.Articles
{
    public interface ICreateOrUpdateArticleCommandService : ICommandService<CreateOrUpdateArticleCommand>
    {
    }
}

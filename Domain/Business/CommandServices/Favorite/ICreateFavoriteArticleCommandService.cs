using Domain.Data.Commands.Favorite;

namespace Domain.Business.CommandServices.Favorite
{
    public interface ICreateFavoriteArticleCommandService : ICommandService<CreateFavoriteArticleCommand>
    {
    }
}

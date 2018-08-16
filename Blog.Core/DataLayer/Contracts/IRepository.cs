using System.Threading.Tasks;

namespace Blog.Core.DataLayer.Contracts
{
    public interface IRepository
    {
        int CommitChanges();

        Task<int> CommitChangesAsync();
    }
}

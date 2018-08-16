using System.Linq;
using Blog.Core.EntityLayer.Dbo;

namespace Blog.Core.DataLayer.Contracts
{
    public interface IEventLogRepository : IRepository
    {
        IQueryable<EventLog> GetEventLogs();

        EventLog GetEventLog(EventLog entity);
    }
}

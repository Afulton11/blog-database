using System.Linq;
using Blog.Core.DataLayer.Contracts;
using Blog.Core.EntityLayer.Dbo;
using Microsoft.EntityFrameworkCore;

namespace Blog.Core.DataLayer.Repositories
{
    public sealed class EventLogRepository : Repository, IEventLogRepository
    {
        public EventLogRepository(IUserInfo userInfo, DbContext dbContext)
            : base(userInfo, dbContext)
        {
        }

        public IQueryable<EventLog> GetEventLogs()
            => DbContext.Set<EventLog>();

        public EventLog GetEventLog(EventLog entity)
            => DbContext.Set<EventLog>().FirstOrDefault(item => item.EventLogID == entity.EventLogID);
    }
}

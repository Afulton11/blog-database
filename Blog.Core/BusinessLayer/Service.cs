using Blog.Core.BusinessLayer.Contracts;
using Blog.Core.DataLayer;
using Blog.Core.DataLayer.Contracts;
using Blog.Core.DataLayer.Repositories;
using Microsoft.Extensions.Logging;

namespace Blog.Core.BusinessLayer
{
    public abstract class Service : IService
    {
        private IEventLogRepository eventLogRepository;
        private IBlogRepository blogRepository;

        public Service(ILogger logger, IUserInfo userInfo, BlogDbContext dbcontext)
        {
            Logger = logger;
            UserInfo = userInfo;
            DbContext = dbcontext;
        }

        public void Dispose()
        {
            if (!Disposed)
            {
                DbContext?.Dispose();

                Disposed = true;
            }
        }

        protected ILogger Logger { get; set; }

        protected IUserInfo UserInfo { get; set; }

        protected BlogDbContext DbContext { get; }

        protected bool Disposed { get; set; }

        protected IEventLogRepository EventLogRepository
            => eventLogRepository ?? (eventLogRepository = new EventLogRepository(UserInfo, DbContext));

        protected IBlogRepository BlogRepository
            => blogRepository ?? (blogRepository = new BlogRepository(UserInfo, DbContext));

    }
}

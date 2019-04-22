using Domain.Data.Queries;
using Domain.Entities.Blog;
using DatabaseFactory.Data.Contracts;
using EnsureThat;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Domain.Business.QueryServices;
using DataAccess.QueryServices.Readers;

namespace DataAccess.DataAccess.QueryServices.UserQueries
{
    public class GetAllUsersQueryService : IQueryService<GetAllUsersQuery, User>
    {
        public IDatabase Database { get; }
        public IReader<User> UserReader { get; }

        public GetAllUsersQueryService(IDatabase database, IReader<User> userReader)
        {
            EnsureArg.IsNotNull(database, nameof(database));
            EnsureArg.IsNotNull(userReader, nameof(userReader));

            Database = database;
            UserReader = userReader;
        }

        private User ReadUsers(IDataReader reader, GetAllUsersQuery query)
        {

        }
    }
}

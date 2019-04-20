﻿using Domain.Data.Queries;
using Domain.Entities.Blog;
using DatabaseFactory.Data.Contracts;
using EnsureThat;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Domain.Business.QueryServices;
using DataAccess.QueryServices.Readers;

namespace DataAccess.QueryServices
{
    public class GetUserByIdQueryService : IQueryService<GetUserByIdQuery, User>
    {
        public IDatabase Database { get; }
        public IReader<User> UserReader { get; }

        public GetUserByIdQueryService(IDatabase database, IReader<User> userReader)
        {
            EnsureArg.IsNotNull(database, nameof(database));
            EnsureArg.IsNotNull(userReader, nameof(userReader));

            Database = database;
            UserReader = userReader;
        }

        public User Execute(GetUserByIdQuery query)
        {
            EnsureArg.IsNotNull(query, nameof(query));

            return Database.TryExecuteTransaction((transaction) =>
            {
                var dbQuery = Database.CreateStoredProcCommand("Blog.GetUserById", transaction);
                var parameter = Database.CreateParameter("UserId", query.UserId);

                dbQuery.Parameters.Add(parameter);

                return Database.ExecuteReader(dbQuery, (reader) => this.ReadUsers(reader, query));
            });
        }

        private User ReadUsers(IDataReader reader, GetUserByIdQuery query)
        {
            var article = UserReader.Read(reader)?.FirstOrDefault() ?? null;

            if (article == null)
            {
                throw new KeyNotFoundException($"No User with Id[{query.UserId}] was found in the database!");
            }

            return article;
        }
    }
}

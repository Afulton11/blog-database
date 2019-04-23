using DataAccess.CommandServices;
using DatabaseFactory.Data.Contracts;
using Domain.Data.Commands;
using System.Collections.Generic;
using System.Data;

namespace DataAccess.DataAccess.CommandServices.Users
{
    public class RemoveUserCommandService : DbCommandService<DeleteUserCommand>
    {
        public RemoveUserCommandService(IDatabase database) : base(database)
        {
        }

        protected override IEnumerable<IDataParameter> GetParameters(DeleteUserCommand command)
        {
            yield return Database.CreateParameter("@UserId", command.UserId);
        }

        protected override string ProcedureName => "Blog.DeleteUser";
    }
}

using DataAccess.CommandServices;
using DatabaseFactory.Data.Contracts;
using Domain.Data.Commands.Roles;
using System.Collections.Generic;
using System.Data;

namespace DataAccess.DataAccess.CommandServices.Roles
{
    public class DeleteRoleCommandService : DbCommandService<DeleteRoleCommand>
    {
        public DeleteRoleCommandService(IDatabase database) : base(database)
        {
        }

        protected override IEnumerable<IDataParameter> GetParameters(DeleteRoleCommand command)
        {
            yield return Database.CreateParameter("RoleId", command.RoleId);
        }

        protected override string ProcedureName => "Blog.DeleteRole";
    }
}

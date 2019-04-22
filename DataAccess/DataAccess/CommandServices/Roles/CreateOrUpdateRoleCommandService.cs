using DataAccess.CommandServices;
using DatabaseFactory.Data.Contracts;
using Domain.Data.Commands.Roles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataAccess.DataAccess.CommandServices.Roles
{
    public class CreateOrUpdateRoleCommandService : DbCommandService<CreateOrUpdateRoleCommand>
    {
        public CreateOrUpdateRoleCommandService(IDatabase database) : base(database)
        {
        }

        protected override IEnumerable<IDataParameter> GetParameters(CreateOrUpdateRoleCommand command)
        {
            if (command.RoleId != null)
            {
                yield return Database.CreateParameter("@RoleId", command.RoleId);
            }
            yield return Database.CreateParameter("@Name", command.Name);
            yield return Database.CreateParameter("@NormalizedName", command.NormalizedName);
        }

        protected override string ProcedureName => "Blog.CreateOrUpdateRole";
    }
}

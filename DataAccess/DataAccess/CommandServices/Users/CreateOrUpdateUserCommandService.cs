using DataAccess.CommandServices;
using DatabaseFactory.Data.Contracts;
using System.Collections.Generic;
using System.Data;
using Domain.Data.Commands;

namespace DataAccess.DataAccess.CommandServices.Users
{
    public class CreateOrUpdateUserCommandService : DbCommandService<CreateOrUpdateUserCommand>
    {
        public CreateOrUpdateUserCommandService(IDatabase database) : base(database)
        {
        }

        protected override IEnumerable<IDataParameter> GetParameters(CreateOrUpdateUserCommand command)
        {
            yield return Database.CreateParameter("@RoleId", command.RoleId);
            yield return Database.CreateParameter("@Username", command.Username);
            yield return Database.CreateParameter("@NormalizedUsername", command.NormalizedUsername);
            yield return Database.CreateParameter("@Password", command.Password);
            yield return Database.CreateParameter("@Email", command.Email);
            yield return Database.CreateParameter("@NormalizedEmail", command.NormalizedEmail);
            yield return Database.CreateParameter("@IsEmailVerified", command.IsEmailVerified);
        }

        protected override string ProcedureName => "Blog.CreateOrUpdateUser";
    }
}

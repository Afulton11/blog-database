using DataAccess.CommandServices;
using DatabaseFactory.Data.Contracts;
using System.Collections.Generic;
using System.Data;
using Domain.Data.Commands;
using Domain.Business.CommandServices;

namespace DataAccess.DataAccess.CommandServices.Users
{
    public class CreateUserCommandService : DbCommandService<CreateUserCommand>
    {
        public CreateUserCommandService(IDatabase database) : base(database)
        {
        }

        protected override IEnumerable<IDataParameter> GetParameters(CreateUserCommand command)
        {
            yield return Database.CreateParameter("@RoleId", command.RoleId);
            yield return Database.CreateParameter("@Username", command.Username);
            yield return Database.CreateParameter("@NormalizedUsername", command.Username.ToUpper());
            yield return Database.CreateParameter("@Password", command.Password);
            yield return Database.CreateParameter("@Email", command.Email);
            yield return Database.CreateParameter("@NormalizedEmail", command.Email.ToUpper());
            yield return Database.CreateParameter("@IsEmailVerified", command.IsEmailVerified);
        }

        protected override string ProcedureName => "Blog.CreateOrUpdateUser";
    }
}

using Domain.Business;
using Domain.Entities.Blog;
using EnsureThat;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Web.Identity
{
    public class RoleStore : IRoleStore<Role>
    {
        private readonly IQueryProcessor queryProcessor;
        private readonly ICommandProcessor commandProcessor;
        public RoleStore(
            IQueryProcessor queryProcessor,
            ICommandProcessor commandProcessor)
        {
            EnsureArg.IsNotNull(queryProcessor, nameof(queryProcessor));
            EnsureArg.IsNotNull(commandProcessor, nameof(commandProcessor));

            this.queryProcessor = queryProcessor;
            this.commandProcessor = commandProcessor;
        }

        public Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            throw new NotImplementedException();
        }

        // TODO: Add RoleId to Domain POCOs
        public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken) =>
            Task.FromResult(role.Name);

        public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken) =>
            Task.FromResult(role.Name);

        public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;
            return Task.FromResult(0);
        }

        public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            // TODO: implement role normalized name in database.
            throw new NotImplementedException();
        }

        public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            // TODO: implement role normalized name in database.
            throw new NotImplementedException();
        }

        public Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            throw new NotImplementedException();
        }

        public Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            // Nothing to dispose
        }
    }
}

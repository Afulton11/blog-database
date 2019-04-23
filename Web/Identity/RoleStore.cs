using Domain.Business;
using Domain.Data.Commands.Roles;
using Domain.Data.Queries.RoleQueries;
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

        public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await commandProcessor.Execute(new CreateOrUpdateRoleCommand
            {
                Name = role.Name,
                NormalizedName = role.NormalizedName,
            });

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await commandProcessor.Execute(new CreateOrUpdateRoleCommand
            {
                RoleId = role.RoleId,
                Name = role.Name,
                NormalizedName = role.NormalizedName,
            });

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await commandProcessor.Execute(new DeleteRoleCommand
            {
                RoleId = role.RoleId
            });

            return IdentityResult.Success;
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

        public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken) =>
            Task.FromResult(role.NormalizedName);

        public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
        {
            role.NormalizedName = normalizedName;
            return Task.FromResult(0);
        }

        public Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return queryProcessor.Execute(new FetchRoleByIdQuery
            {
                RoleId = int.Parse(roleId),
            });
        }

        public Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return queryProcessor.Execute(new FetchRoleByNormalizedNameQuery
            {
                NormalizedName = normalizedRoleName,
            });
        }

        public void Dispose()
        {
            // Nothing to dispose
        }
    }
}

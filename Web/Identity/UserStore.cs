using Domain.Business;
using Domain.Data.Commands;
using Domain.Data.Queries;
using Domain.Data.Queries.UserQueries;
using Domain.Entities.Blog;
using EnsureThat;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Web.Identity
{
    /// <summary>
    /// https://markjohnson.io/articles/asp-net-core-identity-without-entity-framework/
    /// </summary>
    public class UserStore : IUserStore<User>, IUserEmailStore<User>, IUserPasswordStore<User>
    {
        private readonly IQueryProcessor queryProcessor;
        private readonly ICommandProcessor commandProcessor;

        public UserStore(
            IQueryProcessor queryProcessor,
            ICommandProcessor commandProcessor)
        {
            EnsureArg.IsNotNull(queryProcessor, nameof(queryProcessor));
            EnsureArg.IsNotNull(commandProcessor, nameof(commandProcessor));

            this.queryProcessor = queryProcessor;
            this.commandProcessor = commandProcessor;
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await commandProcessor.Execute(new CreateOrUpdateUserCommand
            {
                Username = user.Username,
                NormalizedUsername = user.Username.Normalize().ToUpper(),
                Email = user.Email,
                NormalizedEmail = user.Email.Normalize().ToUpper(),
                IsEmailVerified = user.IsEmailVerified,
                Password = user.Password,
                RoleId = user.RoleId,
            });

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await commandProcessor.Execute(new CreateOrUpdateUserCommand
            {
                Username = user.Username,
                NormalizedUsername = user.Username.Normalize().ToUpper(),
                Email = user.Email,
                NormalizedEmail = user.Email.Normalize().ToUpper(),
                IsEmailVerified = user.IsEmailVerified,
                Password = user.Password,
                RoleId = user.RoleId,
            });

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await commandProcessor.Execute(new DeleteUserCommand
            {
               UserId = user.UserId
            });

            return IdentityResult.Success;
        }

        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return queryProcessor.Execute(new FetchUserByIdQuery
            {
                UserId = int.Parse(userId)
            });
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var name = await queryProcessor.Execute(new FetchUserByNormalizedNameQuery
            {
                NormalizedUsername = normalizedUserName
            });

            return name;
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken) =>
            Task.FromResult(user.UserId.ToString());

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken) =>
            Task.FromResult(user.Username);

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            user.Username = userName;
            return Task.FromResult(0);
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken) =>
            Task.FromResult(user.NormalizedUsername);

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUsername = normalizedName;
            return Task.FromResult(0);
        }

        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken) =>
            Task.FromResult(user.Email);

        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken) =>
            Task.FromResult(user.IsEmailVerified);

        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            user.IsEmailVerified = confirmed;
            return Task.FromResult(0);
        }

        public async Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await queryProcessor.Execute(new FetchUserByNormalizedEmailQuery
            {
                NormalizedEmail = normalizedEmail
            });

            return user;
        }

        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken) =>
            Task.FromResult(user.NormalizedEmail);

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.FromResult(0);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.Password = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken) =>
            Task.FromResult(user.Password);

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken) =>
            Task.FromResult(!string.IsNullOrEmpty(user.Password));

        public void Dispose()
        {
            // Nothing to dispose
        }
    }
}

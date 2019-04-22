using Domain.Business;
using Domain.Entities.Blog;
using EnsureThat;
using Microsoft.AspNetCore.Identity;
using System;
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

        public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            throw new NotImplementedException();
        }

        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            throw new NotImplementedException();
        }

        public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            throw new NotImplementedException();
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            throw new NotImplementedException();
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken) =>
            Task.FromResult(user.Username);

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            // TODO: Add NormalizedUsername (these normalization properties are used to improved performance)
            throw new NotImplementedException();
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

        public Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken) =>
            throw new NotImplementedException();

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            // TODO: Add NormalizedEmail parameter to user email.
            throw new NotImplementedException();
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

using Domain.Data;
using Domain.Entities.Blog;
using EnsureThat;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web
{
    public class AspNetUserContextAdapter : IUserContext
    {
        private readonly IHttpContextAccessor accessor;

        public AspNetUserContextAdapter(IHttpContextAccessor accessor)
        {
            EnsureArg.IsNotNull(accessor);

            this.accessor = accessor;
        }

        public int? CurrentUserId
        {
            get
            {
                int result;
                int.TryParse(accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out result);

                return result >= 0 ? result : (int?)null;
            }
        }

        public bool IsInRole(Role role)
        {
            return accessor.HttpContext.User.IsInRole(role.ToString());
        }
    }
}

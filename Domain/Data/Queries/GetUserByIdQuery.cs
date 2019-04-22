﻿using Domain.Entities.Blog;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Queries
{
    public class GetUserByIdQuery : IQuery<User>
    {
        [Required]
        public int UserId { get; set; }
        public bool WithDeleted { get; set; }
    }
}

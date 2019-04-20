﻿using Domain.Entities.Blog;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Commands.Articles
{
    public class CreateArticleCommand : ICommand
    {
        /// <summary>
        /// The <see cref="Article"/> to create.
        /// </summary>
        [Required]
        public Article Article { get; set;}
    }
}

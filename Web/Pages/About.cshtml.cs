using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Business.QueryServices;
using Core.Data.Queries;
using Core.Entities.Blog;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class AboutModel : PageModel
    {
        public string Message { get; set; }

        public Article Article { get; set; }

        private readonly GetArticleByIdQueryService getArticle;

        public AboutModel(GetArticleByIdQueryService getArticle)
        {
            this.getArticle = getArticle;
        }

        public void OnGet()
        {
            Message = "Your application description page.";
            Article = getArticle.Execute(new GetArticleByIdQuery
            {
                ArticleID = 1
            });
        }
    }
}

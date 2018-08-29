namespace Blog.Core.BusinessLayer
{
    public static class BlogDisplays
    {
        public static string NonExistingArticleExceptionMessage
            => "The Requeted article [{0}] does not exist.";

        public static string CreateArticleMessage
            => "The article [{0}] was successfully created.";

        public static string RemoveArticleMessage
            => "The article [{0}] was successfully removed.";
    }
}

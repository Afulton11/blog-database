namespace Blog.Core.BusinessLayer
{
    public sealed class NonExistingArticleException : BlogException
    {
        public NonExistingArticleException() : base()
        {
        }

        public NonExistingArticleException(string message) : base(message)
        {
        }
    }
}

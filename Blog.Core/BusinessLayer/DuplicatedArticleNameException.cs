namespace Blog.Core.BusinessLayer
{
    public sealed class DuplicatedArticleNameException : BlogException
    {
        public DuplicatedArticleNameException()
        {
        }

        public DuplicatedArticleNameException(string message) : base(message)
        {
        }
    }
}

namespace Blog.Core.BusinessLayer
{
    public sealed class ForeignKeyDependencyException : BlogException
    {
        public ForeignKeyDependencyException()
        {
        }

        public ForeignKeyDependencyException(string message) : base(message)
        {
        }
    }
}

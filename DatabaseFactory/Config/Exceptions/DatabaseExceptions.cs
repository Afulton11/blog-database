using System;
namespace DatabaseFactory.Config.Exceptions
{
    public sealed class DatabaseTypeMismatchException : Exception
    {
        public DatabaseTypeMismatchException(Type expected, Type given) : base()
        {
            this.Expected = expected;
            this.Given = given;
        }

        public Type Expected { get; }
        public Type Given { get; }

        public override string Message =>
            $"Expected given type {Given.ToString()} to be assignable to database type {Expected.ToString()}!";

        }

}

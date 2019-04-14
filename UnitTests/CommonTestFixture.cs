using NUnit.Framework;

namespace UnitTests
{
    /// <summary>
    /// This is the attribute that marks a class that contains the one-time setup or teardown methods for all
    /// the test fixtures under a given namespace.
    /// </summary>
    [SetUpFixture]
    public class CommonTestFixture
    {
        [OneTimeSetUp]
        public void BeforeAnyTests()
        {
            StringFormatters.Add();
        }
    }
}

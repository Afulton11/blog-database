using Newtonsoft.Json;
using NUnit.Framework;

namespace UnitTests
{
    public static class StringFormatters
    {
        public static void Add()
        {
            TestContext.AddFormatter(obj =>
            {
                if (obj == null)
                    return x => "";

                return (x) =>
                {
                    if (x == null)
                        return "null";
                    try
                    {
                        return JsonConvert.SerializeObject(x);
                    }
                    catch (JsonSerializationException)
                    {
                        return "null";
                    }
                };
            });
        }
    }
}

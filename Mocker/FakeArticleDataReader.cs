using System.Collections.Generic;
using System.Linq;

namespace Mocker
{
    public class FakeArticleDataReader : FakeDataReader
    {
        static IEnumerable<IDictionary<string, object>> CreateFakeArticleRows()
        {
            yield return new Dictionary<string, object>
            {
                ["ArticleId"] = 0
            };
        }

        public FakeArticleDataReader()
            : base(CreateFakeArticleRows().ToList())
        {
        }
    }
}

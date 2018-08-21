using System.Collections.Generic;

namespace Blog.Core.BusinessLayer.Responses
{
    public sealed class ListResponse<TModel> : IListResponse<TModel>
    {
        public IEnumerable<TModel> Model { get; set; }

        public string Message { get; set; }

        public bool DidError { get; set; }

        public string ErrorMessage { get; set; }
    }
}

using System.Collections.Generic;

namespace Blog.Core.BusinessLayer.Responses
{
    public interface IListResponse<TModel> : IResponse
    {
        IEnumerable<TModel> Model { get; set; }
    }
}

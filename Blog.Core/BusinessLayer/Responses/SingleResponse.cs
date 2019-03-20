﻿namespace Blog.Core.BusinessLayer.Responses
{
    public class SingleResponse<TModel> : ISingleResponse<TModel>
        where TModel : new()
    {
        public SingleResponse()
        {
            Model = new TModel();
        }

        public TModel Model { get; set; }

        public string Message { get; set; }

        public bool DidError { get; set; }

        public string ErrorMessage { get; set; }
    }
}

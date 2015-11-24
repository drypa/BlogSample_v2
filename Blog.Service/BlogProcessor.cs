using System;
using Blog.Service.Contract;
using Nelibur.ServiceModel.Services.Operations;

namespace Blog.Service
{
    public class BlogProcessor :
        IPost<AddPostRequest>,
        IPost<AddCommentRequest>,
        IDelete<DeletePostRequest>,
        IDelete<DeleteCommentRequest>,
        IGet<GetPostRequest>,
        IGet<GetPostsRequest>
    {
        public object Delete(DeleteCommentRequest request)
        {
            throw new NotImplementedException();
        }

        public object Delete(DeletePostRequest request)
        {
            throw new NotImplementedException();
        }

        public object Get(GetPostRequest request)
        {
            throw new NotImplementedException();
        }

        public object Get(GetPostsRequest request)
        {
            throw new NotImplementedException();
        }

        public object Post(AddCommentRequest request)
        {
            throw new NotImplementedException();
        }

        public object Post(AddPostRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
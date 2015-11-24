using System;
using System.Collections.Generic;
using Blog.BusinessEntities.Contract;
using Blog.Client.Models;
using Nelibur.ServiceModel.Clients;

namespace Blog.Client
{
    public class BlogClient : IBlogClient
    {
        private readonly JsonServiceClient client;
        public BlogClient(string service)
        {
            client = new JsonServiceClient(service);
        }

        public void AddComment(PostComment comment)
        {
            client.Post<AddCommentRequest>(new AddCommentRequest { Text = comment.Text, PostId = comment.Post.Id });
        }

        public void AddPost(Post post)
        {
            throw new NotImplementedException();
//            client.Post<AddPos>(post);
        }

        public void DeleteComment(PostComment comment)
        {
            client.Delete<PostComment>(comment);
        }

        public void DeletePost(Post post)
        {
            client.Delete<Post>(post);
        }

        public PostDetails GetPost(Guid postId)
        {
            throw new NotImplementedException();
//            client.Post<PostComment>(comment);
//            using (ChannelFactory<IBlogService> factory = CreateChanelFactory())
//            {
//                BlogPost blogPost = factory.CreateChannel().GetPost(postId);
//                if (blogPost != null)
//                {
//                    return blogPost.ToPostDetails();
//                }
//                else
//                {
//                    return null;
//                }
//            }
        }

        public List<Post> GetPosts()
        {
            var response = client.Get<PostListResponse>(new GetPostsRequest());
            return response.Posts.ToViewModel();
        }

    }
}

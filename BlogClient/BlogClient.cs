using System;
using System.Collections.Generic;
using Blog.BusinessEntities;
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
            client.Post(new AddPostRequest { Title = post.Title });
        }

        public void DeleteComment(PostComment comment)
        {
            client.Delete(new DeleteCommentRequest { CommentId = comment.Id });
        }

        public void DeletePost(Post post)
        {
            client.Delete(new DeletePostRequest { PostId = post.Id });
        }

        public PostDetails GetPost(Guid postId)
        {
            var post = client.Get<BlogPost>(new GetPostRequest { PostId = postId });
            if (post == null)
            {
                return null;
            }
            return post.ToPostDetails();
        }

        public List<Post> GetPosts()
        {
            var response = client.Get<PostListResponse>(new GetPostsRequest());
            return response.Posts.ToViewModel();
        }

    }
}

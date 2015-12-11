using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Contract;
using Nelibur.ServiceModel.Clients;

namespace Blog.BusinessLogic.Client
{
    public class BlogClient : IBlogClient
    {
        private readonly JsonServiceClient client;
        public BlogClient(string service)
        {
            client = new JsonServiceClient(service);
        }

        public void AddComment(CommentDto comment)
        {
            client.Post(new AddCommentRequest { Text = comment.Text, PostId = comment.Post.Id });
        }

        public void AddPost(BlogPostDto post)
        {
            client.Post(new AddPostRequest { Title = post.Title, Text = post.Text });
        }

        public void DeleteComment(CommentDto comment)
        {
            client.Delete(new DeleteCommentRequest { CommentId = comment.Id });
        }

        public void DeletePost(BlogPostDto post)
        {
            client.Delete(new DeletePostRequest { PostId = post.Id });
        }

        public BlogPostDto GetPost(Guid postId)
        {
            var post = client.Get<BlogPostDto>(new GetPostRequest { PostId = postId });
            if (post == null)
            {
                return null;
            }
            return post;
        }

        public List<BlogPostDto> GetPosts()
        {
            var response = client.Get<PostListResponse>(new GetPostsRequest());
            return response.Posts.ToList();
        }

    }
}

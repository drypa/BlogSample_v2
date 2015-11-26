using System;
using System.Collections.Generic;
using System.Linq;
using Blog.BusinessEntities;
using Blog.BusinessEntities.Contract;
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

        public void AddComment(Comment comment)
        {
            client.Post<AddCommentRequest>(new AddCommentRequest { Text = comment.Text, PostId = comment.Post.Id });
        }

        public void AddPost(BlogPost post)
        {
            client.Post(new AddPostRequest { Title = post.Title });
        }

        public void DeleteComment(Comment comment)
        {
            client.Delete(new DeleteCommentRequest { CommentId = comment.Id });
        }

        public void DeletePost(BlogPost post)
        {
            client.Delete(new DeletePostRequest { PostId = post.Id });
        }

        public BlogPost GetPost(Guid postId)
        {
            var post = client.Get<BlogPost>(new GetPostRequest { PostId = postId });
            if (post == null)
            {
                return null;
            }
            return post;
        }

        public List<BlogPost> GetPosts()
        {
            var response = client.Get<PostListResponse>(new GetPostsRequest());
            return response.Posts.ToList();
        }

    }
}

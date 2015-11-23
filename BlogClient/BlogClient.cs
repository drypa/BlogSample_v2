using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Description;
using Blog.BusinessEntities;
using Blog.Client.Models;

namespace Blog.Client
{
    public class BlogClient : IBlogClient
    {
        private readonly int maxReceivedMessageSize;
        private readonly string serviceUrl;

        public BlogClient(string service, int maxMessageSize)
        {
            serviceUrl = service;
            maxReceivedMessageSize = maxMessageSize;
        }

        public void AddComment(PostComment comment)
        {
            using (ChannelFactory<IBlogService> factory = CreateChanelFactory())
            {
                factory.CreateChannel().AddComment(comment.ToModel());
            }
        }

        public void AddPost(Post post)
        {
            using (ChannelFactory<IBlogService> factory = CreateChanelFactory())
            {
                factory.CreateChannel().AddPost(post.ToModel());
            }
        }

        public void DeleteComment(PostComment comment)
        {
            using (ChannelFactory<IBlogService> factory = CreateChanelFactory())
            {
                factory.CreateChannel().DeleteComment(comment.ToModel());
            }
        }

        public void DeletePost(Post post)
        {
            using (ChannelFactory<IBlogService> factory = CreateChanelFactory())
            {
                factory.CreateChannel().DeletePost(post.ToModel());
            }
        }

        public PostDetails GetPost(Guid postId)
        {
            using (ChannelFactory<IBlogService> factory = CreateChanelFactory())
            {
                BlogPost blogPost = factory.CreateChannel().GetPost(postId);
                if (blogPost != null)
                {
                    return blogPost.ToPostDetails();
                }
                else
                {
                    return null;
                }
            }
        }

        public List<Post> GetPosts()
        {
            using (ChannelFactory<IBlogService> factory = CreateChanelFactory())
            {
                return factory.CreateChannel().GetPosts().ToViewModel();
            }
        }

        private ChannelFactory<IBlogService> CreateChanelFactory()
        {
            WebHttpBinding binding = maxReceivedMessageSize == 0 ? new WebHttpBinding() : new WebHttpBinding { MaxReceivedMessageSize = maxReceivedMessageSize };
            var factory = new ChannelFactory<IBlogService>(binding, serviceUrl);
            factory.Endpoint.Behaviors.Add(new WebHttpBehavior());
            return factory;
        }
    }
}

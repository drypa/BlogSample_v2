using System;
using System.Collections.Generic;
using Blog.BusinessEntities;
using Blog.BusinessLogic;

namespace Blog.Service
{
    public class BlogService : IBlogService
    {
        private IBlogManager _manager;
        private IBlogManager Manager
        {
            get { return _manager ?? (_manager = new NHibernateBlogManager()); }
        }

        public void AddComment(Comment comment)
        {
            Manager.AddComment(comment);
        }

        public void AddPost(BlogPost post)
        {
            Manager.AddPost(post);
        }

        public void DeletePost(BlogPost post)
        {
            Manager.DeletePost(post);
        }

        public BlogPost GetPost(Guid postId)
        {
            return Manager.GetPost(postId);
        }

        public IList<BlogPost> GetPosts()
        {
            return Manager.GetPosts();
        }
    }
}

using System;
using System.Collections.Generic;
using Blog.BusinessEntities;
using Blog.BusinessLogic;

namespace Blog.Service
{
    public class BlogService : IBlogService
    {
        private IBlogManager _nhManager;

        private IBlogManager NHManager
        {
            get { return _nhManager ?? (_nhManager = new NHibernateBlogManager()); }
        }
        private IBlogManager _dapperManager;

        private IBlogManager DapperManager
        {
            get { return _dapperManager ?? (_dapperManager = new DapperBlogManager()); }
        }

        public void AddComment(Comment comment)
        {
            NHManager.AddComment(comment);
        }

        public void AddPost(BlogPost post)
        {
            NHManager.AddPost(post);
        }

        public void DeletePost(BlogPost post)
        {
            NHManager.DeletePost(post);
        }

        public BlogPost GetPost(Guid postId)
        {
            return DapperManager.GetPost(postId);
        }

        public IList<BlogPost> GetPosts()
        {
            return DapperManager.GetPosts();
        }
    }
}

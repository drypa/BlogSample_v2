using System;
using System.Collections.Generic;
using Blog.BusinessEntities;
using Blog.BusinessLogic;

namespace Blog.Service
{
    public class BlogService : IBlogService
    {
        private IBlogRepository nhRepository;

        private IBlogRepository NhRepository
        {
            get { return nhRepository ?? (nhRepository = new NHibernateBlogRepository(new AppSettingsHelper())); }
        }
        private IBlogRepository dapperRepository;

        private IBlogRepository DapperRepository
        {
            get { return dapperRepository ?? (dapperRepository = new DapperBlogRepository(new AppSettingsHelper())); }
        }

        public void AddComment(Comment comment)
        {
            NhRepository.AddComment(comment);
        }

        public void AddPost(BlogPost post)
        {
            NhRepository.AddPost(post);
        }

        public void DeletePost(BlogPost post)
        {
            NhRepository.DeletePost(post);
        }

        public BlogPost GetPost(Guid postId)
        {
            return DapperRepository.GetPost(postId);
        }

        public IList<BlogPost> GetPosts()
        {
            return DapperRepository.GetPosts();
        }
    }
}

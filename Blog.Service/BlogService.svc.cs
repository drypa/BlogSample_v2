using System;
using System.Collections.Generic;
using Blog.BusinessEntities;
using Blog.BusinessLogic;

namespace Blog.Service
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository readBlogRepository;

        private readonly IBlogRepository writeBlogRepository;

        public BlogService(IBlogRepository readRepository, IBlogRepository writeRepository)
        {
            readBlogRepository = readRepository;
            writeBlogRepository = writeRepository;
        }

        public void AddComment(Comment comment)
        {
            writeBlogRepository.AddComment(comment);
        }

        public void AddPost(BlogPost post)
        {
            writeBlogRepository.AddPost(post);
        }

        public void DeleteComment(Comment comment)
        {
            writeBlogRepository.DeleteComment(comment);
        }

        public void DeletePost(BlogPost post)
        {
            writeBlogRepository.DeletePost(post);
        }

        public IList<Comment> GetComments(Guid postId)
        {
            return readBlogRepository.GetComments(postId);
        }

        public BlogPost GetPost(Guid postId)
        {
            return readBlogRepository.GetPost(postId);
        }

        public IList<BlogPost> GetPosts()
        {
            return readBlogRepository.GetPosts();
        }
    }
}

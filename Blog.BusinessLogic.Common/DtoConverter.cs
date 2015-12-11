using System;
using System.Collections.Generic;
using System.Linq;
using Blog.BusinessEntities;
using Blog.Contract;

namespace Blog.BusinessLogic.Common
{
    public static class DtoConverter
    {
        public static BlogPostDto ToDto(this BlogPost post)
        {
            return new BlogPostDto
            {
                Id = post.Id,
                Title = post.Title,
                Text = post.Text,
                CreateDate = post.CreateDate,
                Comments = post.Comments == null ? null : post.Comments.ToDto()
            };
        }

        public static IList<BlogPostDto> ToDto(this IList<BlogPost> posts)
        {
            return posts.Select(x => x.ToDto()).ToList();
        }

        public static IList<CommentDto> ToDto(this IList<Comment> comments)
        {
            return comments.Select(x => x.ToDto()).ToList();
        }

        public static CommentDto ToDto(this Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                Text = comment.Text,
                Post = new BlogPostDto { Id = comment.Post.Id },
                CreateDate = comment.CreateDate
            };
        }

        public static BlogPost ToModel(this BlogPostDto post)
        {
            return new BlogPost
            {
                Id = post.Id,
                Title = post.Title,
                Text = post.Text,
                CreateDate = post.CreateDate,
                Comments = post.Comments.ToModel()
            };
        }

        public static IList<BlogPost> ToModel(this IList<BlogPostDto> posts)
        {
            return posts.Select(x => x.ToModel()).ToList();
        }

        public static IList<Comment> ToModel(this IList<CommentDto> comments)
        {
            return comments.Select(x => x.ToModel()).ToList();
        }

        public static Comment ToModel(this CommentDto comment)
        {
            return new Comment
            {
                Id = comment.Id,
                Text = comment.Text,
                Post = new BlogPost { Id = comment.Post.Id },
                CreateDate = comment.CreateDate
            };
        }
    }
}

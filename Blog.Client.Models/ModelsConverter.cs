using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Client.Common.Model;
using Blog.Contract;

namespace Blog.Client.Common
{
    internal static class ModelsConverter
    {
        public static BlogPostDto ToDto(this PostDetails details)
        {
            return new BlogPostDto
            {
                Text = details.Text,
                Title = details.Title,
                CreateDate = details.CreationDate
            };
        }

        public static BlogPostDto ToDto(this Post post)
        {
            return new BlogPostDto
            {
                Id = post.Id,
                Title = post.Title
            };
        }

        public static CommentDto ToDto(this PostComment comment)
        {
            return new CommentDto
            {
                Post = comment.Post.ToDto(),
                Text = comment.Text,
                CreateDate = comment.CreationDate,
                Id = comment.Id
            };
        }

        public static PostDetails ToPostDetails(this BlogPostDto post)
        {
            return new PostDetails
            {
                Id = post.Id,
                Title = post.Title,
                Text = post.Text,
                CreationDate = post.CreateDate,
                Comments = post.Comments.ToViewModel()
            };
        }

        public static List<Post> ToViewModel(this IEnumerable<BlogPostDto> posts)
        {
            return posts.Select(x => x.ToViewModel()).ToList();
        }

        public static List<PostComment> ToViewModel(this IEnumerable<CommentDto> comments)
        {
            return comments.Select(x => x.ToViewModel()).ToList();
        }

        public static PostComment ToViewModel(this CommentDto comment)
        {
            return new PostComment
            {
                Id = comment.Id,
                Text = comment.Text,
                CreationDate = comment.CreateDate
            };
        }

        public static Post ToViewModel(this BlogPostDto post)
        {
            return new Post
            {
                Id = post.Id,
                Title = post.Title
            };
        }
    }
}

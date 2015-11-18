using System;
using System.Collections.Generic;
using System.Linq;
using Blog.BusinessEntities;
using Blog.Client.Models;

namespace Blog.Client
{
    public static class ModelsConverter
    {
        public static BlogPost ToModel(this PostDetails details)
        {
            return new BlogPost
            {
                Text = details.Text,
                Title = details.Title,
                CreateDate = details.CreationDate
            };
        }

        public static List<Post> ToViewModel(this IEnumerable<BlogPost> posts)
        {
            return posts.Select(x => x.ToViewModel()).ToList();
        }

        public static List<PostComment> ToViewModel(this IEnumerable<Comment> comments)
        {
            return comments.Select(x => x.ToViewModel()).ToList();
        }

        public static PostComment ToViewModel(this Comment comment)
        {
            return new PostComment
            {
                Id = comment.Id,
                Text = comment.Text,
                CreationDate = comment.CreateDate
            };
        }

        public static Post ToViewModel(this BlogPost post)
        {
            return new Post
            {
                Id = post.Id,
                Title = post.Title
            };
        }

        public static PostDetails ToPostDetails(this BlogPost post)
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

        public static BlogPost ToModel(this Post post)
        {
            return new BlogPost
            {
                Id = post.Id,
                Title = post.Title
            };
        }
    }
}

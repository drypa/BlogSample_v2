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
            return posts.Select(x=>x.ToViewModel()).ToList();
        }

        public static Post ToViewModel(this BlogPost post)
        {
            return new Post
            {
                Id = post.Id,
                Title = post.Title
            };
        } 
    }
}

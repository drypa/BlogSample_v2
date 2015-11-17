using System;
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
    }
}

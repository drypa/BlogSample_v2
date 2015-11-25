using System;
using System.Collections.Generic;
using Blog.BusinessEntities;

namespace Blog.BusinessLogic
{
    public interface IBlogReader
    {
        /// <summary>
        /// Получить статью вместе с коментариями по её идентификатору
        /// </summary>
        /// <param name="postId">Идентификатор статьи</param>
        /// <returns>статья с коментариями</returns>
        BlogPost GetPost(Guid postId);

        /// <summary>
        /// Получить список всех статей
        /// </summary>
        /// <returns>Список всех статей</returns>
        IList<BlogPost> GetPosts();
    }
}
using System;
using System.Collections.Generic;
using Blog.BusinessEntities;

namespace Blog.BusinessLogic
{
    public interface IBlogRepository
    {
        /// <summary>
        /// Добавить новый комментарий
        /// </summary>
        /// <param name="comment">Новый коментарий</param>
        void AddComment(Comment comment);
        /// <summary>
        /// Добавить новую статью
        /// </summary>
        /// <param name="post">Новая статья</param>
        void AddPost(BlogPost post);
        /// <summary>
        /// Удалить статью
        /// </summary>
        /// <param name="post">Удаляемая статья</param>
        void DeletePost(BlogPost post);
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

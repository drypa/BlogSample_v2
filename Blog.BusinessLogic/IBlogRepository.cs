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
        /// Удалить коментарий
        /// </summary>
        /// <param name="comment">Удаляемый коментарий</param>
        void DeleteComment(Comment comment);

        /// <summary>
        /// Удалить статью
        /// </summary>
        /// <param name="post">Удаляемая статья</param>
        void DeletePost(BlogPost post);

        /// <summary>
        /// Получить список коментариев для статьи
        /// </summary>
        /// <param name="postId">Идентификатор статьи, для которой получаем коментарии</param>
        /// <returns>Список коментариев для статьи</returns>
        IList<Comment> GetComments(Guid postId);

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

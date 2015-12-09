using System;
using Blog.BusinessEntities;

namespace Blog.BusinessLogic
{
    public interface IBlogWriter
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
    }
}
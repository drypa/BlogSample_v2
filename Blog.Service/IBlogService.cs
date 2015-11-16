using System;
using System.Collections.Generic;
using System.ServiceModel;
using Blog.BusinessEntities;

namespace Blog.Service
{
    [ServiceContract]
    public interface IBlogService
    {
        /// <summary>
        /// Добавить новый комментарий
        /// </summary>
        /// <param name="comment">Новый коментарий</param>
        [OperationContract]
        void AddComment(Comment comment);
        /// <summary>
        /// Добавить новую статью
        /// </summary>
        /// <param name="post">Новая статья</param>
        [OperationContract]
        void AddPost(BlogPost post);
        /// <summary>
        /// Удалить статью
        /// </summary>
        /// <param name="post">Удаляемая статья</param>
        [OperationContract]
        void DeletePost(BlogPost post);
        /// <summary>
        /// Получить статью вместе с коментариями по её идентификатору
        /// </summary>
        /// <param name="postId">Идентификатор статьи</param>
        /// <returns>статья с коментариями</returns>
        [OperationContract]
        BlogPost GetPost(Guid postId);
        /// <summary>
        /// Получить список всех статей
        /// </summary>
        /// <returns>Список всех статей</returns>
        [OperationContract]
        IList<BlogPost> GetPosts();
    }
}

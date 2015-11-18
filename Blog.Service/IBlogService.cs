using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
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
        /// Удалить коментарий
        /// </summary>
        /// <param name="comment">Удаляемый коментарий</param>
        [OperationContract]
        void DeleteComment(Comment comment);

        /// <summary>
        /// Удалить статью
        /// </summary>
        /// <param name="post">Удаляемая статья</param>
        [OperationContract]
        void DeletePost(BlogPost post);

        /// <summary>
        /// Получить список коментариев для статьи
        /// </summary>
        /// <param name="postId">Идентификатор статьи, для которой получаем коментарии</param>
        /// <returns>Список коментариев для статьи</returns>
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        IList<Comment> GetComments(Guid postId);

        /// <summary>
        /// Получить статью вместе с коментариями по её идентификатору
        /// </summary>
        /// <param name="postId">Идентификатор статьи</param>
        /// <returns>статья с коментариями</returns>
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        BlogPost GetPost(Guid postId);

        /// <summary>
        /// Получить список всех статей
        /// </summary>
        /// <returns>Список всех статей</returns>
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        IList<BlogPost> GetPosts();
    }
}

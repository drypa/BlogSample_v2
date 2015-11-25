using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Blog.Client.Annotations;

namespace Blog.Client.Models
{
    public sealed class BlogViewModel : INotifyPropertyChanged
    {
        private readonly IBlogClient blogClient;
        private readonly PostComment newComment = new PostComment();
        private readonly IClientNotificator clientNotificator;

        private PostDetails currentPost;
        private bool hasNewPost;
        private List<Post> posts;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="client" >Клиент получения данных</param>
        /// <param name="notificator">Сервис, позволяющий информировать пользователя</param>
        public BlogViewModel(IBlogClient client, IClientNotificator notificator)
        {
            blogClient = client;
            clientNotificator = notificator;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand AddCommentCommand
        {
            get { return new RelayCommand(AddComment, CanAddComment); }
        }

        public ICommand AddPostCommand
        {
            get { return new RelayCommand(x => AddNewPost(), x => HasNewPost); }
        }

        public PostDetails CurrentPost
        {
            get { return currentPost; }
            private set
            {
                if (currentPost != value)
                {
                    currentPost = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand DeleteCommentCommand
        {
            get { return new RelayCommand(DeleteComment, x => true); }
        }

        public ICommand DeletePostCommand
        {
            get { return new RelayCommand(DeletePost, x => true); }
        }

        public PostComment NewComment
        {
            get { return newComment; }
        }

        public ICommand NewPostCommand
        {
            get { return new RelayCommand(x => CreateNewPost(), x => true); }
        }

        public List<Post> Posts
        {
            get
            {
                if (posts == null)
                {
                    posts = blogClient.GetPosts();
                    OnPropertyChanged();
                }
                return posts;
            }
            set
            {
                if (posts != value)
                {
                    posts = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand SelectEmptyPostCommand
        {
            get { return new RelayCommand(x => LoadPostDetails(new Post()), x => true); }
        }

        public ICommand SelectPostCommand
        {
            get { return new RelayCommand(LoadPostDetails, x => true); }
        }

        private bool HasNewPost
        {
            get { return hasNewPost; }
            set
            {
                if (hasNewPost != value)
                {
                    hasNewPost = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand RefreshCommand
        {
            get { return new RelayCommand(Refresh, x => true); }
        }

        private void Refresh(object obj)
        {
            Posts = blogClient.GetPosts();
            if (CurrentPost != null)
            {
                CurrentPost = blogClient.GetPost(CurrentPost.Id);
            }
        }

        private void AddComment(object obj)
        {
            NewComment.CreationDate = DateTime.Now;
            NewComment.Post = CurrentPost;

            blogClient.AddComment(NewComment);

            LoadPostDetails(CurrentPost);
            NewComment.Text = string.Empty;
        }

        private void AddNewPost()
        {
            CurrentPost.CreationDate = DateTime.Now;
            blogClient.AddPost(CurrentPost);
            Posts = blogClient.GetPosts();
            HasNewPost = false;
        }

        private bool CanAddComment(object obj)
        {
            return currentPost != null && NewComment != null && !string.IsNullOrEmpty(NewComment.Text);
        }

        private void CreateNewPost()
        {
            HasNewPost = true;
            CurrentPost = new PostDetails();
        }

        private void DeleteComment(object obj)
        {
            var comment = (obj as PostComment);
            comment.Post = CurrentPost;
            blogClient.DeleteComment(comment);

            LoadPostDetails(CurrentPost);
        }

        private void DeletePost(object obj)
        {
            var post = (obj as Post);
            blogClient.DeletePost(post);
            Posts = blogClient.GetPosts();
            if (Posts.Count > 0)
            {
                LoadPostDetails(Posts.First());
            }
        }

        private void LoadPostDetails(object post)
        {
            Guid postId = (post as Post).Id;
            PostDetails loadedPost = blogClient.GetPost(postId);
            if (loadedPost == null)
            {
                clientNotificator.ShowMessage("Статья не найдена");
            }
            else
            {
                CurrentPost = loadedPost;
            }
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

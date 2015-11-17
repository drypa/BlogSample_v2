using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Windows.Input;
using Blog.Client.Annotations;

namespace Blog.Client.Models
{
    public sealed class BlogViewModel : INotifyPropertyChanged
    {
        private ICommand addPostCommand;
        private ICommand newPostCommand;
        private PostDetails currentPost;
        private bool hasNewPost;
        private List<Post> posts;
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand NewPostCommand
        {
            get
            {
                if (newPostCommand == null)
                {
                    newPostCommand = new RelayCommand(x => CreateNewPost(), x => true);
                }
                return newPostCommand;
            }
        }

        private void CreateNewPost()
        {
            HasNewPost = true;
            CurrentPost = new PostDetails();
        }

        public ICommand AddPostCommand
        {
            get
            {
                if (addPostCommand == null)
                {
                    addPostCommand = new RelayCommand(x => AddNewPost(), x => HasNewPost);
                }
                return addPostCommand;
            }
        }

        public PostDetails CurrentPost
        {
            get { return currentPost; }
            set
            {
                if (currentPost != value)
                {
                    currentPost = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool HasNewPost
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

        public ICommand DeletePostCommand
        {
            get { throw new NotImplementedException(); }
        }

        public List<Post> Posts
        {
            get
            {
                if (posts == null)
                {
                    posts = GetPosts();
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
        private List<Post> GetPosts()
        {
            using (var factory = CreateChanelFactory())
            {
                return factory.CreateChannel().GetPosts().ToViewModel();
            }
        } 

        public ICommand SelectPostCommand
        {
            get { throw new NotImplementedException(); }
        }

        private void AddNewPost()
        {
            var model = CurrentPost.ToModel();
            model.CreateDate = DateTime.Now;
            using (var factory = CreateChanelFactory())
            {
                factory.CreateChannel().AddPost(model);
            }
            
            HasNewPost = false;
        }



        private ChannelFactory<IBlogService> CreateChanelFactory()
        {
            ChannelFactory<IBlogService> factory = new ChannelFactory<IBlogService>(
                new WebHttpBinding(), ConfigurationManager.AppSettings["SeviceUrl"]);
            factory.Endpoint.Behaviors.Add(new WebHttpBehavior());
            return factory;
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

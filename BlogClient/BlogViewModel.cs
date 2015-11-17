using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Blog.Client.Annotations;

namespace Blog.Client
{
    public class BlogViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand AddPostCommand
        {
            get { throw new NotImplementedException(); }
        }

        public PostDetails CurrentPost { get; set; }

        public ICommand DeletePostCommand
        {
            get { throw new NotImplementedException(); }
        }

        public List<Post> Posts { get; set; }

        public ICommand SelectPostCommand
        {
            get { throw new NotImplementedException(); }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

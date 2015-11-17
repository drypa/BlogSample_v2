using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Blog.Client.Annotations;

namespace Blog.Client
{
    public class Post : INotifyPropertyChanged
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

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

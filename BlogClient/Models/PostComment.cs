using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Blog.Client.Annotations;

namespace Blog.Client.Models
{
    public class PostComment : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public DateTime CreationDate { get; set; }
        public Guid Id { get; set; }
        public string Text { get; set; }

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

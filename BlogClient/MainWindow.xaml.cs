using System;
using System.Windows;
using Blog.Client.Models;

namespace Blog.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public BlogViewModel ViewModel { get; set; }
        public MainWindow()
        {
            var alert = (Action<string>)((msg) => MessageBox.Show(msg));
            ViewModel = new BlogViewModel(alert);
            InitializeComponent();
        }
    }
}

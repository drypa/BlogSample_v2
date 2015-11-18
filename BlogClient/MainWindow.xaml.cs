using System;
using System.Configuration;
using System.Windows;
using Blog.Client.Models;

namespace Blog.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var alert = (Action<string>)((msg) => MessageBox.Show(msg));
            string serviceUrl = ConfigurationManager.AppSettings["SeviceUrl"];
            ViewModel = new BlogViewModel(serviceUrl, alert);
            InitializeComponent();
        }

        public BlogViewModel ViewModel { get; set; }
    }
}

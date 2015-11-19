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
            int maxReceivedMessageSize;
            int.TryParse(ConfigurationManager.AppSettings["MaxReceivedMessageSize"], out maxReceivedMessageSize);
            ViewModel = new BlogViewModel(serviceUrl, maxReceivedMessageSize, alert);
            InitializeComponent();
        }

        public BlogViewModel ViewModel { get; set; }
    }
}

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

            var client = new BusinessLogic.Client.BlogClient(serviceUrl);
            var notificator = new ClientNotificator(alert);
            ViewModel = new BlogViewModel(client, notificator);
            InitializeComponent();
        }

        public BlogViewModel ViewModel { get; set; }
    }
}

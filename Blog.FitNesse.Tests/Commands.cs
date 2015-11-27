using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using fitlibrary;

namespace Blog.FitNesse.Tests
{
    public class Commands : DoFixture, IDisposable
    {
        private static string connectionString;
        private static Process serviceProcess;
        private static string serviceUrl;
        private static Process workerProcess;
        private SqlConnection connection;

        public static string ConnectionString
        {
            get { return connectionString; }
        }

        public static string ServiceUrl
        {
            get { return serviceUrl; }
        }

        public void CleanComments()
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "truncate table [dbo].[Comment];";
                command.ExecuteNonQuery();
            }
            Console.WriteLine("Comments table was cleaned");
        }

        public void CleanPosts()
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "delete from [dbo].[BlogPost];";
                command.ExecuteNonQuery();
            }
            Console.WriteLine("BlogPosts table was cleaned");
        }

        public void CloseConnection()
        {
            if (connection != null)
            {
                connection.Close();
                Console.WriteLine("Connection was closed");
            }
        }

        public void OpenConnection(string connectionStr)
        {
            connectionString = connectionStr;
            connection = new SqlConnection(connectionString);
            connection.Open();
            Console.WriteLine("Connection was opened");
        }

        public void SetServiceUrl(string url)
        {
            serviceUrl = url;
        }

        public void StartService(string servicePath)
        {
            serviceProcess = new Process
            {
                StartInfo = new ProcessStartInfo(servicePath, serviceUrl)
            };
            serviceProcess.Start();
            Console.WriteLine("serviceProcess" + serviceProcess.StartTime);
        }

        public void StartWorker(string workerPath)
        {
            workerProcess = StartProcess(workerPath);
            Console.WriteLine("workerProcess" + workerProcess.StartTime);
        }

        public void StopService()
        {
            if (serviceProcess != null)
            {
                serviceProcess.CloseMainWindow();
                serviceProcess.Close();
                Console.WriteLine("serviceProcess closing");
            }
        }

        public void StopWorker()
        {
            if (workerProcess != null)
            {
                workerProcess.CloseMainWindow();
                workerProcess.Close();
                Console.WriteLine("workerProcess closing");
            }
        }

        public void UpdateBlogConnectionString(string configPath)
        {
            var doc = XDocument.Load(configPath);
            XmlNamespaceManager xnm = new XmlNamespaceManager(new NameTable());
            xnm.AddNamespace("x", "http://demo.com/2011/demo-schema");
            var el = doc.XPathSelectElements("/configuration/connectionStrings/add[1]");
            el.Attributes("connectionString").First().Value = ConnectionString;
            doc.Save(configPath);
        }

        public void Dispose()
        {
            StopService();
            StopWorker();
            CloseConnection();
        }

        private Process StartProcess(string executablePath)
        {
            return Process.Start(executablePath);
        }
    }
}

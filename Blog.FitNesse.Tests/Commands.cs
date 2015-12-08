using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using Blog.Test.Common;
using fitlibrary;

namespace Blog.FitNesse.Tests
{
    public class Commands : DoFixture, IDisposable
    {
        private static string connectionString;
        private static Process serviceProcess;
        private static string serviceUrl;
        private static Process workerProcess;

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
            new TestRepository(ConnectionString).CleanComments();
            Console.WriteLine("Comments table was cleaned");
        }

        public void CleanPosts()
        {
            new TestRepository(ConnectionString).CleanPosts();
            Console.WriteLine("BlogPosts table was cleaned");
        }

        public void OpenConnection(string connectionStr)
        {
            connectionString = connectionStr;
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
            XDocument doc = XDocument.Load(configPath);
            var xnm = new XmlNamespaceManager(new NameTable());
            xnm.AddNamespace("x", "http://demo.com/2011/demo-schema");
            IEnumerable<XElement> el = doc.XPathSelectElements("/configuration/connectionStrings/add[1]");
            el.Attributes("connectionString").First().Value = ConnectionString;
            doc.Save(configPath);
        }

        public void Dispose()
        {
            StopService();
            StopWorker();
        }

        private Process StartProcess(string executablePath)
        {
            return Process.Start(executablePath);
        }
    }
}

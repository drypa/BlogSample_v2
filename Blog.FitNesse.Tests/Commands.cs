using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using fitlibrary;

namespace Blog.FitNesse.Tests
{
    public class Commands : DoFixture, IDisposable
    {
        private SqlConnection connection;
        private static Process serviceProcess;
        private static Process workerProcess;

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

        public void OpenConnection(string connectionString)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            Console.WriteLine("Connection was opened");
        }

        public void StartService(string servicePath)
        {
            serviceProcess = StartProcess(servicePath);
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

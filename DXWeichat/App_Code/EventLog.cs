using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;

public class Log
{
    /// <summary>
    /// Event Log create
    /// Update by Henry Huang
    /// Update Time 2015-01-24
    /// </summary>
    #region Variables
    private static string logFile = AppDomain.CurrentDomain.BaseDirectory + @"/log/Log.txt";
    private static StreamWriter writer;
    private static FileStream fileStream = null;
    #endregion

    public Log()
    {

    }

    public static void log(string info)
    {
        if (CreateDirectory(logFile))
        {
            try
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(logFile);
                if (!fileInfo.Exists)
                {
                    fileStream = fileInfo.Create();
                    writer = new StreamWriter(fileStream);
                }
                else
                {
                    fileStream = fileInfo.Open(FileMode.Append, FileAccess.Write);
                    writer = new StreamWriter(fileStream);
                }
                writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": " + info);

            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                    writer.Dispose();
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }
        }
    }

    public static bool CreateDirectory(string infoPath)
    {
        bool status = true;
        try
        {
            DirectoryInfo directoryInfo = Directory.GetParent(infoPath);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
        }
        catch (Exception ex)
        {
            status = false;
        }
        return status;
    }
}

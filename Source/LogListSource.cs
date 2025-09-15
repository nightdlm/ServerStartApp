using System.Collections.ObjectModel;
using WpfAppAi.Model;

namespace WpfAppAi.Source
{
    class LogListSource
    {

        // 最大日志大小
        public static int MaxLogSize = 1000;

        private static ObservableCollection<LogEntry> LogList { get; set; } = [];

        // 添加日志
        public static void AddLog(LogEntry logEntry)
        {
            LogList.Add(logEntry);
            if (LogList.Count > MaxLogSize)
            {
                LogList.RemoveAt(0);
            }
        }


        // 清空日志
        public static void ClearLog()
        {
            LogList.Clear();
        }


        public static ObservableCollection<LogEntry> GetInstance()
        {
            return LogList;
        }

    }

}

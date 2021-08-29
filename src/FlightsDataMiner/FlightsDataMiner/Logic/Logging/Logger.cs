using System;
using System.IO;

namespace FlightsDataMiner.Logic.Logging
{
    /// <summary>
    /// Логгер
    /// </summary>
    class Logger
    {
        #region Fields

        private StreamWriter _logWriter;

        #endregion

        #region Properties

        private string LogFilename
        {
            get { return string.Format("Logs\\FlightsDataMiner_{0:dd.MM.yyyy}.txt", DateTime.Now); }
        }

        /// <summary>
        /// Путь до файла логов
        /// </summary>
        private string LogFilePath
        {
            get { return Path.Combine(Directory.GetCurrentDirectory(), LogFilename); }
        }

        #endregion

        public Logger()
        {
            var logsDirectory = Path.GetDirectoryName(LogFilePath);
            if (!Directory.Exists(logsDirectory))
                Directory.CreateDirectory(logsDirectory);

            _logWriter = new StreamWriter(LogFilePath, true);
        }

        /// <summary>
        /// Логгирование сообщения
        /// </summary>
        /// <param name="message"></param>
        public void Log(string message, EventType eventType)
        {
            var output = string.Format("{0:dd-MM-yyyy HH:mm:ss.fff}|{1}|{2}", DateTime.Now, getMessageTag(eventType),
                message);

            try
            {
                _logWriter.WriteLine(output);
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// Закрытие _logWriter
        /// </summary>
        public void Unload()
        {
            _logWriter.Close();
        }

        /// <summary>
        /// Получить тэн по eventType
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns></returns>
        private string getMessageTag(EventType eventType)
        {
            switch (eventType)
            {
                case EventType.Notify:
                    return "INFO";
                case EventType.Warning:
                    return "WARNING";
                case EventType.Error:
                    return "ERROR";
                default:
                    throw new ArgumentOutOfRangeException("eventType", eventType, null);
            }
        }
    }
}

using System;

namespace FlightsDataMiner.Logic.Logging
{
    /// <summary>
    /// Класс для работы с логгированием
    /// </summary>
    public class Logging
    {
        private static Logging _instance;

        private static Logger _logger;

        public Logging()
        {
            _logger = new Logger();
        }

        public static Logging Instance()
        {
            return _instance ??= new Logging();
        }

        /// <summary>
        /// Выгрузка логгера
        /// </summary>
        public static void Unload()
        {
            if (_logger != null)
                _logger.Unload();
        }

        /// <summary>
        /// Запись информации в логи
        /// </summary>
        /// <param name="text">Текст сообщения</param>
        public void LogNotification(string text)
        {
            _logger.Log(text, EventType.Notify);
        }

        /// <summary>
        /// Запись предупреждения в логи
        /// </summary>
        /// <param name="text">Текст предупреждения</param>
        public void LogWarning(string text)
        {
            _logger.Log(text, EventType.Warning);
        }

        public void LogWarning(Exception exception)
        {
            _logger.Log("Exception info: " + exception, EventType.Warning);
        }

        public void LogWarning(string text, Exception exception)
        {
            _logger.Log("Comment: " + text + "\nException info: " + exception, EventType.Warning);
        }

        /// <summary>
        /// Запись ошибки в логи
        /// </summary>
        /// <param name="text">Текст ошибки</param>
        public void LogError(string text)
        {
            _logger.Log(text, EventType.Error);
        }

        /// <summary>
        /// Запись исключения в логи
        /// </summary>
        /// <param name="exception">Исключение для записи</param>
        public void LogError(Exception exception)
        {
            _logger.Log("Exception info: " + exception, EventType.Error);
        }

        /// <summary>
        /// Запись в логи исключения с комментарием
        /// </summary>
        /// <param name="text">Комментарий к ошибке</param>
        /// <param name="exception">Исключение для записи</param>
        public void LogError(string text, Exception exception)
        {
            _logger.Log("Comment: " + text + "\nException info: " + exception, EventType.Error);
        }
    }
}

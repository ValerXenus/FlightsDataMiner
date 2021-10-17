using System;
using System.Configuration;
using System.Globalization;

namespace FlightsDataMiner.Logic
{
    /// <summary>
    /// Класс для работы с настройками из файла App.config
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Путь до .exe-файла браузера Chrome
        /// </summary>
        public string ChromePath { get; init; }

        /// <summary>
        /// Текущая дата авиариейсов
        /// </summary>
        public DateTime CurrentDate { get; private set; }

        /// <summary>
        /// Признак загрузки HTML-контента из сохраненного файла
        /// True - загрузка будет осуществляться из файла в папка RawData
        /// False - загрузка будет осуществляться из сайта (по умолчанию)
        /// </summary>
        public bool LoadHtmlFromFile { get; private set; }

        /// <summary>
        /// Конструктор класса настроек
        /// </summary>
        /// <param name="args">Агрументы командной строки</param>
        public AppSettings(string[] args)
        {
            Logging.Logging.Instance().LogNotification("Загрузка настроек...");

            ChromePath = getSafelySetting("ChromBrowserPath");
            parseCommandLineArgs(args);

            Logging.Logging.Instance().LogNotification("Настройки успешно загружены");
        }

        /// <summary>
        /// Безопасное получение настройки из файла App.config по названию
        /// </summary>
        /// <param name="settingName">Наименование настройки</param>
        /// <returns></returns>
        private string getSafelySetting(string settingName)
        {
            return ConfigurationManager.AppSettings[settingName] ?? "";
        }

        /// <summary>
        /// Распарсить аргументы командной строки
        /// </summary>
        /// <param name="args"></param>
        private void parseCommandLineArgs(string[] args)
        {
            if (args.Length == 0)
            {
                setArgumentDefaults();
                return;
            }

            CurrentDate = getCurrentDate(args[0]);

            if (args.Length > 1 && args[1].ToLower().Equals("-f"))
                LoadHtmlFromFile = true;

        }

        /// <summary>
        /// Установка значенипо умолчанию для настроек,
        /// значение которых должно быть обязательно заполнено
        /// </summary>
        private void setArgumentDefaults()
        {
            CurrentDate = DateTime.Now.Date;
        }

        /// <summary>
        /// Получить текущую дату
        /// </summary>
        /// <param name="stringDate">Агрументы из Main</param>
        /// <returns></returns>
        private DateTime getCurrentDate(string stringDate)
        {
            if (DateTime.TryParseExact(stringDate, "dd.MM.yyyy", new CultureInfo("ru-RU"), DateTimeStyles.None,
                out var parseDate))
                return parseDate;

            Logging.Logging.Instance()
                .LogWarning($"Не удалось распарсить текущую дату \"{stringDate}\". В качесве текущей установлена дата: \"{DateTime.Now.Date:MM.dd.yyyy}\".");
            return DateTime.Now.Date;
        }
    }
}

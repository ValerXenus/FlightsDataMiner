using System;
using System.IO;
using FlightsDataMiner.Base.Common;
using FlightsDataMiner.Base.Common.Enums;
using FlightsDataMiner.Logic;
using FlightsDataMiner.Logic.Logging;

namespace FlightsDataMiner.Data
{
    /// <summary>
    /// Получение данных из сайта аэропорта
    /// </summary>
    public class FlightsDataAccess
    {
        /// <summary>
        /// Настройки приложения
        /// </summary>
        private readonly AppSettings _settings;

        public FlightsDataAccess(AppSettings settings)
        {
            _settings = settings;
        }

        /// <summary>
        /// Получить HTML-контент страницы онлайн-табло
        /// </summary>
        /// <param name="mode">Режим чтения (прилет/вылет)</param>
        /// <returns></returns>
        public string LoadFlightsHtml(FlightsReadMode mode)
        {
            return _settings.LoadHtmlFromFile 
                ? loadFlightsHtmlFromFile(mode) 
                : loadFlightsFromWebsite(mode);
        }

        /// <summary>
        /// Получить HTML-контент страницы онлайн-табло из Сайта
        /// </summary>
        /// <param name="mode">Режим чтения (прилет/вылет)</param>
        /// <returns></returns>
        private string loadFlightsFromWebsite(FlightsReadMode mode)
        {
            string flightsUrl;
            switch (mode)
            {
                case FlightsReadMode.Departures:
                    flightsUrl = StringConstants.DeparturesUrl;
                    break;
                case FlightsReadMode.Arrivals:
                    flightsUrl = StringConstants.ArrivalsUrl;
                    break;
                default:
                    return string.Empty;
            }

            var jsContent = Resources.setFullFlightsJs;
            using var browser = new HeadlessBrowser(_settings);

            return browser.GetHtmlContent(flightsUrl, jsContent);
        }

        /// <summary>
        /// Получить HTML-контент страницы онлайн-табло из сохраненного HTML-файла
        /// <param name="mode">Режим чтения (прилет/вылет)</param>
        /// </summary>
        /// <returns></returns>
        private string loadFlightsHtmlFromFile(FlightsReadMode mode)
        {
            var rawDataPath = Path.Combine(AppContext.BaseDirectory, "RawData");
            if (!Directory.Exists(rawDataPath))
            {
                Logging.Instance().LogError("Папка RawData в папке с утилитой не была найдена");
                return string.Empty;
            }

            string flightsFile;
            switch (mode)
            {
                case FlightsReadMode.Departures:
                    flightsFile = Path.Combine(rawDataPath, StringConstants.DeparturesFilename);
                    break;
                case FlightsReadMode.Arrivals:
                    flightsFile = Path.Combine(rawDataPath, StringConstants.ArrivalsFilename);
                    break;
                default:
                    return string.Empty;
            }

            if (!File.Exists(flightsFile))
            {
                Logging.Instance()
                    .LogError($"Не хватает файла \"{flightsFile}\"");
                return string.Empty;
            }

            var flightsContent = File.ReadAllText(flightsFile);
            if (!string.IsNullOrEmpty(flightsContent)) 
                return flightsContent;

            Logging.Instance().LogError($"Файл \"{flightsFile}\" пуст");
            return string.Empty;

        }
    }
}

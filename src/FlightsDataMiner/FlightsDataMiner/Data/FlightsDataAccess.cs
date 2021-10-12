using System;
using System.IO;
using FlightsDataMiner.Base.Common;
using FlightsDataMiner.Logic.Logging;

namespace FlightsDataMiner.Data
{
    /// <summary>
    /// Получение данных из сайта аэропорта
    /// </summary>
    public class FlightsDataAccess
    {
        /// <summary>
        /// Получить HTML-контент страницы с информацией о рейсах
        /// </summary>
        /// <returns></returns>
        public string LoadFlightsHtmlFromFile(FileReadMode mode)
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
                case FileReadMode.Departures:
                    flightsFile = Path.Combine(rawDataPath, StringConstants.DeparturesFilename);
                    break;
                case FileReadMode.Arrivals:
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
            if (string.IsNullOrEmpty(flightsContent))
            {
                Logging.Instance()
                    .LogError($"Файл \"{flightsFile}\" пуст");
                return string.Empty;
            }

            return flightsContent;
        }
    }
}

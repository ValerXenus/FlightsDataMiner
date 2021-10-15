using System.Collections.Generic;
using FlightsDataMiner.Base.Common;
using FlightsDataMiner.Base.Common.Enums;
using FlightsDataMiner.Base.Objects;
using HtmlAgilityPack;

namespace FlightsDataMiner.Logic
{
    /// <summary>
    /// Класс для парсинга HTML
    /// </summary>
    public class HtmlParser
    {
        /// <summary>
        /// HTML-документ Departures
        /// </summary>
        private readonly HtmlDocument _departuresDocument;

        /// <summary>
        /// HTML-документ Arrivals
        /// </summary>
        private readonly HtmlDocument _arrivalsDocument;

        /// <summary>
        /// Парсер списка рейсов
        /// </summary>
        private readonly FlightsParser _flightsParser;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="departuresHtml">HTML содержимое вылетов в виде строки</param>
        /// <param name="arrivalsHtml">HTML содержимое прилетов в виде строки</param>
        public HtmlParser(string departuresHtml, string arrivalsHtml)
        {
            Logging.Logging.Instance().LogNotification("Инициализация HTML парсера");

            _departuresDocument = new HtmlDocument();
            _departuresDocument.LoadHtml(departuresHtml);

            _arrivalsDocument = new HtmlDocument();
            _arrivalsDocument.LoadHtml(arrivalsHtml);

            _flightsParser = new FlightsParser();
            Logging.Logging.Instance().LogNotification("HTML парсер успешно инициализирован");
        }

        /// <summary>
        /// Главный парсер списка авиарейсов
        /// </summary>
        /// <param name="directionType">Тип рейса (Departure/Arrival)</param>
        /// <returns></returns>
        public List<FlightInfo> GetFlights(DirectionType directionType)
        {
            // Default direction type - Departure
            var logWord = "ВЫЛЕТАЮЩИХ";
            var flightsDocument = _departuresDocument;

            if (directionType == DirectionType.Arrival)
            {
                logWord = "ПРИЛЕТАЮЩИХ";
                flightsDocument = _arrivalsDocument;
            }

            Logging.Logging.Instance().LogNotification($"Запуск парсинга списка {logWord} рейсов");
            var nodes = flightsDocument.DocumentNode.SelectNodes(StringConstants.FlightsXPath);
            return nodes == null
                ? new List<FlightInfo>()
                : _flightsParser.ParseFlightsRows(nodes, directionType);
        }
    }
}

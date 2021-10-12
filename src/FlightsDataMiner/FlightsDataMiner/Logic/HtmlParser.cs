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
        /// Главный парсер списка "Вылет"
        /// </summary>
        /// <returns></returns>
        public List<FlightInfo> GetDepartureFlights()
        {
            Logging.Logging.Instance().LogNotification("Запуск парсинга списка ВЫЛЕТАЮЩИХ рейсов");
            var nodes = _departuresDocument.DocumentNode.SelectNodes(StringConstants.DepartureFlightsXPath);
            return nodes == null 
                ? new List<FlightInfo>() 
                : _flightsParser.ParseFlights(nodes, DirectionType.Departure);
        }

        /// <summary>
        /// Главный парсер списка "Прилет"
        /// </summary>
        /// <returns></returns>
        public List<FlightInfo> GetArrivalFlights()
        {
            Logging.Logging.Instance().LogNotification("Запуск парсинга списка ПРИЛЕТАЮЩИХ рейсов");
            var nodes = _arrivalsDocument.DocumentNode.SelectNodes(StringConstants.ArrivalFlightsXPath);
            return nodes == null
                ? new List<FlightInfo>()
                : _flightsParser.ParseFlights(nodes, DirectionType.Arrival);
        }
    }
}

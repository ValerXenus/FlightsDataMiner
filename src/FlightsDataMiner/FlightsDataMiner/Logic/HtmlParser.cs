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
        /// Поле объекта HTML-документа
        /// </summary>
        private readonly HtmlDocument _htmlDocument;

        /// <summary>
        /// Парсер списка рейсов
        /// </summary>
        private readonly FlightsParser _flightsParser;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="htmlContent">HTML содержимое в виде строки</param>
        public HtmlParser(string htmlContent)
        {
            Logging.Logging.Instance().LogNotification("Инициализация HTML парсера");
            _htmlDocument = new HtmlDocument();
            _htmlDocument.LoadHtml(htmlContent);
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
            var nodes = _htmlDocument.DocumentNode.SelectNodes(StringConstants.DepartureFlightsXPath);
            return _flightsParser.ParseFlights(nodes, DirectionType.Departure);
        }

        /// <summary>
        /// Главный парсер списка "Прилет"
        /// </summary>
        /// <returns></returns>
        public List<FlightInfo> GetArrivalFlights()
        {
            Logging.Logging.Instance().LogNotification("Запуск парсинга списка ПРИЛЕТАЮЩИХ рейсов");
            var nodes = _htmlDocument.DocumentNode.SelectNodes(StringConstants.ArrivalFlightsXPath);
            return _flightsParser.ParseFlights(nodes, DirectionType.Arrival);
        }
    }
}

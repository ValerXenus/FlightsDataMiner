using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FlightsDataMiner.Base.Common;
using FlightsDataMiner.Base.Common.Enums;
using FlightsDataMiner.Base.Helpers;
using FlightsDataMiner.Base.Objects;
using HtmlAgilityPack;

namespace FlightsDataMiner.Logic
{
    /// <summary>
    /// Парсер таблицы со списком рейсов
    /// </summary>
    public class FlightsParser
    {
        /// <summary>
        /// Дата, по которой необходимо отбирать авиарейсы
        /// </summary>
        private readonly DateTime _currentDate;

        public FlightsParser(DateTime currentDate)
        {
            _currentDate = currentDate;
        }

        #region Public methods

        /// <summary>
        /// Парсинг списка рейсов
        /// </summary>
        /// <param name="nodes">Список HTML-узлов рейсов</param>
        /// <param name="direction">Направление полета (Вылет/Прилет)</param>
        /// <returns></returns>
        public List<FlightInfo> ParseFlightsRows(HtmlNodeCollection nodes, DirectionType direction)
        {
            return nodes
                .Select(node => ParseFlight(node, direction))
                .Where(flight => flight != null)
                .ToList();
        }

        /// <summary>
        /// Парсинг информации о рейсе
        /// </summary>
        /// <param name="node">HTML-узел рейса</param>
        /// <param name="direction">Направление полета (Вылет/Прилет)</param>
        /// <returns></returns>
        public FlightInfo ParseFlight(HtmlNode node, DirectionType direction)
        {
            if (!checkFlightStatus(node) || !checkFlightIsToday(node))
                return null;

            var outcome = new FlightInfo
            {
                DirectionType = direction,
                FlightNumber = parseFlightNumber(node),
                Airline = parseAirline(node),
                PlaneType = parsePlaneType(node),
                Destination = parseCityName(node),
                ScheduledDateTime = parseScheduledDateTime(node, TimeType.Scheduled),
                ActualDateTime = parseScheduledDateTime(node, TimeType.Actual)
            };

            return outcome.CheckDataValid() ? outcome : null;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Проверка статуса полета
        /// Отбираем только вылетевшие/прилетевшие рейсы
        /// </summary>
        /// <param name="node">HTML-узел рейса</param>
        /// <returns></returns>
        private bool checkFlightStatus(HtmlNode node)
        {
            var status = node.SelectSingleNode(StringConstants.FlightStatusXPath).InnerText;
            if (string.IsNullOrEmpty(status))
                return false;

            return status.TrimStart().TrimEnd().ToLower().StartsWith("вылетел")
                || status.TrimStart().TrimEnd().ToLower().StartsWith("прибыл");
        }

        /// <summary>
        /// Проверка, что полет осуществляется сегодня
        /// Отбираем только сегодняшние рейсы
        /// </summary>
        /// <param name="node">HTML-узел рейса</param>
        /// <returns></returns>
        private bool checkFlightIsToday(HtmlNode node)
        {
            var flightDateInfo = node.SelectSingleNode(StringConstants.FlightDateTimeXPath);
            var realFlightDateTimeNode = flightDateInfo.SelectSingleNode(StringConstants.ActualDateTimeXPath) 
                                         ?? flightDateInfo.SelectSingleNode(StringConstants.CoincidentDateTimeXPath);

            // If date string still empty
            if (realFlightDateTimeNode == null)
                return false;

            var realDate = parseFlightDate(realFlightDateTimeNode.InnerText, false);
            var currentDate = _currentDate;

            return realDate == currentDate;
        }

        /// <summary>
        /// Парсинг номера рейса
        /// </summary>
        /// <param name="node">HTML-узел рейса</param>
        /// <returns></returns>
        private string parseFlightNumber(HtmlNode node)
        {
            var flightNumber = node.SelectSingleNode(StringConstants.FlightNumberXPath).InnerText;
            return string.IsNullOrEmpty(flightNumber)
                ? string.Empty
                : flightNumber.TrimStart().TrimEnd();
        }

        /// <summary>
        /// Парсинг авиакомпании
        /// </summary>
        /// <param name="node">HTML-узел рейса</param>
        /// <returns></returns>
        private Airlines parseAirline(HtmlNode node)
        {
            var airlineInfo = node.SelectSingleNode(StringConstants.AirlineXPath);
            if (airlineInfo == null)
            {
                Logging.Logging.Instance().LogError($"Не удалось получить название авиакомпании {node.InnerText}");
                return default;
            }

            var name = airlineInfo.Attributes["title"].Value;
            if (!string.IsNullOrEmpty(name))
                return EnumTranslator.GetValueFromDescription<Airlines>(name.ToUpper());

            Logging.Logging.Instance().LogError($"Авиакомпания {airlineInfo} не найдена в справочнике");
            return default;
        }

        /// <summary>
        /// Парсинг модели самолета
        /// </summary>
        /// <param name="node">HTML-узел рейса</param>
        /// <returns></returns>
        private PlaneTypes parsePlaneType(HtmlNode node)
        {
            // Unfortunately plane type is no longer supported in data
            return PlaneTypes.None;

            //var planeName = node.SelectSingleNode(StringConstants.PlaneNameXPath).InnerText;
            //if (!string.IsNullOrEmpty(planeName))
            //    return EnumTranslator.GetValueFromDescription<PlaneTypes>(planeName);

            //Logging.Logging.Instance().LogError($"Модель самолета {planeName} не найдена в справочнике");
            //return default;
        }

        /// <summary>
        /// Парсинг города назначения
        /// </summary>
        /// <param name="node">HTML-узел рейса</param>
        /// <returns></returns>
        private string parseCityName(HtmlNode node)
        {
            var destinationName = node.SelectSingleNode(StringConstants.DestinationXPath).InnerText;
            return string.IsNullOrEmpty(destinationName)
                ? string.Empty
                : destinationName.TrimStart().TrimEnd();
        }

        /// <summary>
        /// Парсинг даты и времени (вылета/прибытия) по расписанию
        /// </summary>
        /// <param name="node">HTML-узел рейса</param>
        /// <param name="timeType">Тип получаемого времени</param>
        /// <returns></returns>
        private DateTime parseScheduledDateTime(HtmlNode node, TimeType timeType)
        {
            var flightDateInfo = node.SelectSingleNode(StringConstants.FlightDateTimeXPath);

            // Актуальная дата/время - значение по умолчанию
            var dateTimeXPath = StringConstants.ActualDateTimeXPath;
            if (timeType == TimeType.Scheduled)
                dateTimeXPath = StringConstants.ScheduledDateTimeXPath;

            var time = flightDateInfo.SelectSingleNode(dateTimeXPath) 
                       ?? flightDateInfo.SelectSingleNode(StringConstants.CoincidentDateTimeXPath);
            if (time == null)
                return DateTime.MinValue;

            var flightDateTime = time.InnerText;

            return string.IsNullOrEmpty(flightDateTime)
                ? DateTime.MinValue
                : parseFlightDate(flightDateTime);
        }

        /// <summary>
        /// Парсинг даты вылета/прилета из таблицы
        /// </summary>
        /// <param name="inputDate">Дата в виде строки</param>
        /// <param name="useDayTime">Признак необходимости парсинга времени</param>
        /// <returns></returns>
        private DateTime parseFlightDate(string inputDate, bool useDayTime = true)
        {
            inputDate = inputDate.TrimStart().TrimEnd();
            var datePattern = "dd.MM.yyyy HH:mm";

            if (!useDayTime)
            {
                datePattern = "dd.MM.yyyy";
                inputDate = inputDate.Split(' ')[0];
            }

            if (DateTime.TryParseExact(inputDate, datePattern, new CultureInfo("ru-RU"),
                DateTimeStyles.None, out var parsedDate))
                return parsedDate;

            Logging.Logging.Instance().LogError($"Не удалось распарсить дату: {inputDate}");
            return DateTime.MinValue;
        }

        #endregion

    }
}

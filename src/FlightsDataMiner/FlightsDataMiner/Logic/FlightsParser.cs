using System;
using System.Collections.Generic;
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
        #region Public methods

        /// <summary>
        /// Парсинг списка рейсов
        /// </summary>
        /// <param name="nodes">Список HTML-узлов рейсов</param>
        /// <param name="direction">Направление полета (Вылет/Прилет)</param>
        /// <returns></returns>
        public List<FlightInfo> ParseFlights(HtmlNodeCollection nodes, DirectionType direction)
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
            if (!checkFlightStatus(node) || !checkFlightIsToday(node, direction))
                return null;

            var outcome = new FlightInfo
            {
                DirectionType = direction,
                FlightNumber = parseFlightNumber(node),
                Airline = parseAirline(node),
                PlaneType = parsePlaneType(node),
                Destination = parseDestination(node),
                ScheduledDateTime = parseScheduledDateTime(node, direction),
                ActualDateTime = parseActualDateTime(node)
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

            return status.TrimStart().TrimEnd().ToLower().Equals("вылетел")
                || status.TrimStart().TrimEnd().ToLower().Equals("прибыл");
        }

        /// <summary>
        /// Проверка, что полет осуществляется сегодня
        /// Отбираем только сегодняшние рейсы
        /// </summary>
        /// <param name="node">HTML-узел рейса</param>
        /// <param name="direction">Направление</param>
        /// <returns></returns>
        private bool checkFlightIsToday(HtmlNode node, DirectionType direction)
        {
            var flightDateXPath = direction == DirectionType.Departure
                ? StringConstants.DepartureDateXPath
                : StringConstants.ArrivalDateXPath;
            var flightDateText = node.SelectSingleNode(flightDateXPath).InnerText;
            if (string.IsNullOrEmpty(flightDateText))
                return false;

            var flightDate = parseFlightDate(flightDateText.Trim());
            var currentDate = DateTime.Now.Date;

            return flightDate == currentDate;
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
            var airlineText = node.SelectSingleNode(StringConstants.AirlineXPath).Attributes["alt"].Value;
            if (!string.IsNullOrEmpty(airlineText))
                return EnumTranslator.GetValueFromDescription<Airlines>(airlineText);

            Logging.Logging.Instance().LogError($"Авиакомпания {airlineText} не найдена в справочнике");
            return default;
        }

        /// <summary>
        /// Парсинг модели самолета
        /// </summary>
        /// <param name="node">HTML-узел рейса</param>
        /// <returns></returns>
        private PlaneTypes parsePlaneType(HtmlNode node)
        {
            var planeName = node.SelectSingleNode(StringConstants.PlaneNameXPath).InnerText;
            if (!string.IsNullOrEmpty(planeName))
                return EnumTranslator.GetValueFromDescription<PlaneTypes>(planeName);

            Logging.Logging.Instance().LogError($"Модель самолета {planeName} не найдена в справочнике");
            return default;
        }

        /// <summary>
        /// Парсинг города назначения
        /// </summary>
        /// <param name="node">HTML-узел рейса</param>
        /// <returns></returns>
        private string parseDestination(HtmlNode node)
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
        /// <param name="direction">Направление</param>
        /// <returns></returns>
        private DateTime parseScheduledDateTime(HtmlNode node, DirectionType direction)
        {
            var timeXPath = direction == DirectionType.Departure
                ? StringConstants.ScheduledDepartureTimeXPath
                : StringConstants.ScheduledArrivalTimeXPath;
            var time = node.SelectSingleNode(timeXPath).InnerText;

            return string.IsNullOrEmpty(time)
                ? DateTime.MinValue
                : parseTime(time.TrimStart().TrimEnd());
        }

        /// <summary>
        /// Парсинг даты и времени (вылета/прибытия) реального
        /// </summary>
        /// <param name="node">HTML-узел рейса</param>
        /// <returns></returns>
        private DateTime parseActualDateTime(HtmlNode node)
        {
            var time = node.SelectSingleNode(StringConstants.ActualTimeXPath).InnerText;
            return string.IsNullOrEmpty(time)
                ? DateTime.MinValue
                : parseTime(time.TrimStart().TrimEnd());
        }

        /// <summary>
        /// Конвертация текстового времени в DateTime
        /// </summary>
        /// <param name="inputTime">Текстовое время</param>
        /// <returns></returns>
        private DateTime parseTime(string inputTime)
        {
            if (inputTime.Length > 5)
            {
                Logging.Logging.Instance().LogNotification($"Парсинг даты и времени прилета: {inputTime}");
                if (!DateTime.TryParse(inputTime, out var outcome))
                    return DateTime.MinValue;

                Logging.Logging.Instance().LogNotification("Парсинг даты и времени прилета успешно завершен");
                return outcome;
            }

            var timeValues = inputTime.Split(":");
            if (!int.TryParse(timeValues[0], out var hour) || !int.TryParse(timeValues[1], out var minute))
            {
                Logging.Logging.Instance().LogError($"Не удалось распарсить время '{inputTime}'");
                return DateTime.MinValue;
            }

            return new DateTime(
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day,
                hour, minute, 0);
        }

        /// <summary>
        /// Парсинг даты вылета/прилета из таблицы
        /// </summary>
        /// <param name="inputDate">Дата в виде строки</param>
        /// <returns></returns>
        private DateTime parseFlightDate(string inputDate)
        {
            inputDate += "." + DateTime.Now.Year;
            if (DateTime.TryParse(inputDate, out var parsedDate)) 
                return parsedDate;

            Logging.Logging.Instance().LogError($"Не удалось распарсить дату: {inputDate}");
            return DateTime.MinValue;
        }

        #endregion

    }
}

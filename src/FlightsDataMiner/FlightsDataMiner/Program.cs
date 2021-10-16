using System;
using System.Collections.Generic;
using System.Globalization;
using FlightsDataMiner.Base.Common.Enums;
using FlightsDataMiner.Base.Objects;
using FlightsDataMiner.Data;
using FlightsDataMiner.Logic;
using FlightsDataMiner.Logic.Logging;

namespace FlightsDataMiner
{
    class Program
    {
        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="args">0 - current date in format dd.MM.yyyy</param>
        /// <returns></returns>
        static int Main(string[] args)
        {
            Console.WriteLine("Started mining data...");

            var currentDate = getCurrentDate(args);

            Logging.Instance().LogNotification("[Запуск сборщика данных]");
            getFlightsData(out var departures, out var arrivals, currentDate);

            var metarAccess = new MetarDataAccess(currentDate);
            var metarData = metarAccess.GetMetarData();

            var fileWriter = new DataFileWriter(departures, arrivals, metarData);
            fileWriter.SaveDataSet();

            Logging.Instance().LogNotification("[Завершение работы сборщика данных]\n\n");

            Console.WriteLine("Everything is ok.");
            return exitApp(0);
        }

        /// <summary>
        /// Получить текущую дату
        /// </summary>
        /// <param name="args">Агрументы из Main</param>
        /// <returns></returns>
        private static DateTime getCurrentDate(string[] args)
        {
            if (args.Length == 0)
                return DateTime.Now.Date;

            if (DateTime.TryParseExact(args[0], "dd.MM.yyyy", new CultureInfo("ru-RU"), DateTimeStyles.None,
                out var parseDate)) 
                return parseDate;

            Logging.Instance()
                .LogWarning($"Не удалось распарсить текущую дату \"{args[0]}\". В качесве текущей установлена дата: \"{DateTime.Now.Date:MM.dd.yyyy}\".");
            return DateTime.Now.Date;

        }

        /// <summary>
        /// Попытки получения данных о рейсах
        /// Всего попыток = 5
        /// </summary>
        /// <param name="departures">Список вылетевших рейсов</param>
        /// <param name="arrivals">Список прилетевших рейсов</param>
        /// <param name="currentDate">Дата, по которой необходимо отбирать авиарейсы</param>
        private static void getFlightsData(out List<FlightInfo> departures, out List<FlightInfo> arrivals, DateTime currentDate)
        {
            var dataAccess = new FlightsDataAccess();

            var departuresHtml = dataAccess.LoadFlightsHtmlFromFile(FileReadMode.Departures);
            var arrivalsHtml = dataAccess.LoadFlightsHtmlFromFile(FileReadMode.Arrivals);
            var htmlParser = new HtmlParser(departuresHtml, arrivalsHtml, currentDate);

            departures = htmlParser.GetFlights(DirectionType.Departure);
            arrivals = htmlParser.GetFlights(DirectionType.Arrival);
        }

        /// <summary>
        /// Метод завершения программы
        /// </summary>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        private static int exitApp(int statusCode)
        {
            Logging.Unload();
            return statusCode;
        }
    }
}

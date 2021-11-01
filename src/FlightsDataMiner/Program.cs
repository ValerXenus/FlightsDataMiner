using System;
using System.Collections.Generic;
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

            var settings = new AppSettings(args);

            Logging.Instance().LogNotification("[Запуск сборщика данных]");
            getFlightsData(out var departures, out var arrivals, settings);

            var metarAccess = new MetarDataAccess(settings);
            var metarData = metarAccess.GetMetarData();

            var fileWriter = new DataFileWriter(departures, arrivals, metarData);
            fileWriter.SaveDataSet();

            Logging.Instance().LogNotification("[Завершение работы сборщика данных]\n\n");

            Console.WriteLine("Everything is ok.");
            return exitApp(0);
        }

        /// <summary>
        /// Попытки получения данных о рейсах
        /// Всего попыток = 5
        /// </summary>
        /// <param name="departures">Список вылетевших рейсов</param>
        /// <param name="arrivals">Список прилетевших рейсов</param>
        /// <param name="settings">Настройки программы</param>
        private static void getFlightsData(out List<FlightInfo> departures, out List<FlightInfo> arrivals, AppSettings settings)
        {
            var dataAccess = new FlightsDataAccess(settings);

            var departuresHtml = dataAccess.LoadFlightsHtml(FlightsReadMode.Departures);
            var arrivalsHtml = dataAccess.LoadFlightsHtml(FlightsReadMode.Arrivals);
            var htmlParser = new HtmlParser(departuresHtml, arrivalsHtml, settings);

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

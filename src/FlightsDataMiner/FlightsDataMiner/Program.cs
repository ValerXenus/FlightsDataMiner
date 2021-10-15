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
        static int Main(string[] args)
        {
            Console.WriteLine("Started mining data...");

            Logging.Instance().LogNotification("[Запуск сборщика данных]");
            getFlightsData(out var departures, out var arrivals);

            var metarAccess = new MetarDataAccess();
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
        private static void getFlightsData(out List<FlightInfo> departures, out List<FlightInfo> arrivals)
        {
            var attempts = 0;
            const int maxAttempts = 5;

            Logging.Instance().LogNotification($"Попытка получения списка рейсов {attempts++} из {maxAttempts}");

            var dataAccess = new FlightsDataAccess();

            var departuresHtml = dataAccess.LoadFlightsHtmlFromFile(FileReadMode.Departures);
            var arrivalsHtml = dataAccess.LoadFlightsHtmlFromFile(FileReadMode.Arrivals);
            var htmlParser = new HtmlParser(departuresHtml, arrivalsHtml);

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

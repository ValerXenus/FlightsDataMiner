using System;
using System.Collections.Generic;
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
            Console.WriteLine("Mining data. Please wait...");

            Logging.Instance().LogNotification("[Запуск сборщика данных]");
            if (!getFlightsData(out var departures, out var arrivals))
                return exitApp(1);

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
        private static bool getFlightsData(out List<FlightInfo> departures, out List<FlightInfo> arrivals)
        {
            var attempts = 0;
            const int maxAttempts = 5;

            departures = new List<FlightInfo>();
            arrivals = new List<FlightInfo>();

            while (departures.Count == 0 || arrivals.Count == 0)
            {
                if (attempts >= maxAttempts)
                {
                    Logging.Instance().LogError($"Данные не удалось получить спустя {maxAttempts} попыток");
                    return false;
                }

                Logging.Instance().LogNotification($"Попытка получения списка рейсов {attempts++} из {maxAttempts}");

                var dataAccess = new FlightsDataAccess();
                var html = dataAccess.GetDashboardHtml();
                var htmlParser = new HtmlParser(html);

                departures = htmlParser.GetDepartureFlights();
                arrivals = htmlParser.GetArrivalFlights();
            }

            return true;
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

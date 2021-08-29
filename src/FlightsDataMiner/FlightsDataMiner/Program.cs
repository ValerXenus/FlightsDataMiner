using System;
using FlightsDataMiner.Data;
using FlightsDataMiner.Logic;
using FlightsDataMiner.Logic.Logging;

namespace FlightsDataMiner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Mining data. Please wait...");

            Logging.Instance().LogNotification("[Запуск сборщика данных]");
            var dataAccess = new FlightsDataAccess();
            var html = dataAccess.GetDashboardHtml();
            var htmlParser = new HtmlParser(html);

            var metarAccess = new MetarDataAccess();
            var metarData = metarAccess.GetMetarData();

            var departures = htmlParser.GetDepartureFlights();
            var arrivals = htmlParser.GetArrivalFlights();

            if (departures.Count == 0 || arrivals.Count == 0)
                Logging.Instance().LogError("Данные о рейсах не удалось прочитать");

            var fileWriter = new DataFileWriter(departures, arrivals, metarData);
            fileWriter.SaveDataSet();

            Logging.Instance().LogNotification("[Завершение работы сборщика данных]\n\n");
            Logging.Unload();
        }
    }
}

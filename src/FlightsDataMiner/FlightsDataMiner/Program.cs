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
            var dataAccess = new DataAccess();
            var html = dataAccess.GetDashboardHtml();
            var parser = new HtmlParser(html);
            var departures = parser.GetDepartureFlights();
            var arrivals = parser.GetArrivalFlights();

            var fileWriter = new DataFileWriter(departures, arrivals);
            fileWriter.SaveDataSet();

            Logging.Instance().LogNotification("[Завершение работы сборщика данных]\n\n");
            Logging.Unload();
        }
    }
}

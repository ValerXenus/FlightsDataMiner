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
            var dataAccess = new DataAccess();
            var html = dataAccess.GetDashboardHtml();
            var parser = new HtmlParser(html);
            var departures = parser.GetDepartureFlights();
            var arrivals = parser.GetArrivalFlights();

            Logging.Unload();

            Console.WriteLine("Данные получены и сохранены");
            Console.ReadKey();
        }
    }
}

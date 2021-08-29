using System.Collections.Generic;
using System.IO;
using FlightsDataMiner.Common;
using FlightsDataMiner.Logic;
using FlightsDataMiner.Logic.Logging;
using FlightsDataMiner.Objects;

namespace FlightsDataMiner.Data
{
    /// <summary>
    /// Запись датасета в файл
    /// </summary>
    public class DataFileWriter
    {
        /// <summary>
        /// Список вылетевших рейсов
        /// </summary>
        private readonly List<FlightInfo> _departureFlights;

        /// <summary>
        /// Список прибывших рейсов
        /// </summary>
        private readonly List<FlightInfo> _arrivalFlights;

        public DataFileWriter(List<FlightInfo> departureFlights, List<FlightInfo> arrivalFlights)
        {
            _departureFlights = departureFlights;
            _arrivalFlights = arrivalFlights;
        }

        /// <summary>
        /// Сохранение датасета
        /// </summary>
        public void SaveDataSet()
        {
            Logging.Instance().LogNotification("Запуск процесса сохранения данных");
            saveDataFile(StringConstants.DeparturesFile, DirectionType.Departure);
            saveDataFile(StringConstants.ArrivalsFile, DirectionType.Arrival);
            Logging.Instance().LogNotification("Данные успешно сохранены");
        }


        /// <summary>
        /// Запись данных в файл
        /// </summary>
        /// <param name="filePath">Путь до файла</param>
        /// <param name="direction">Направление</param>
        private void saveDataFile(string filePath, DirectionType direction)
        {
            var directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            var flights = direction == DirectionType.Departure
                ? _departureFlights
                : _arrivalFlights;

            using var writer = new StreamWriter(filePath, true);
            foreach (var flight in flights)
            {
                writer.WriteLine(getFlightRow(flight));
            }
        }

        /// <summary>
        /// Получить строку с данными о рейсе
        /// </summary>
        /// <param name="flightInfo">Данные о рейсе</param>
        /// <returns></returns>
        private string getFlightRow(FlightInfo flightInfo)
        {
            return string.Concat(
                flightInfo.FlightNumber, "|", 
                EnumTranslator.GetEnumDescription(flightInfo.Airline), "|",
                EnumTranslator.GetEnumDescription(flightInfo.PlaneType), "|",
                flightInfo.Destination, "|", 
                flightInfo.ScheduledDateTime.ToString("dd.MM.yyyy HH:mm:ss"), "|", 
                flightInfo.ActualDateTime.ToString("dd.MM.yyyy HH:mm:ss"));
        }
    }
}

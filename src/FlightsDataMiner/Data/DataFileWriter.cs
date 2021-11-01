using System.Collections.Generic;
using System.IO;
using FlightsDataMiner.Base.Common;
using FlightsDataMiner.Base.Common.Enums;
using FlightsDataMiner.Base.Helpers;
using FlightsDataMiner.Base.Objects;
using FlightsDataMiner.Logic.Logging;

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

        /// <summary>
        /// Список погодных данных
        /// </summary>
        private readonly List<string> _rawMetars;

        public DataFileWriter(List<FlightInfo> departureFlights, List<FlightInfo> arrivalFlights, List<string> rawMetars)
        {
            _departureFlights = departureFlights;
            _arrivalFlights = arrivalFlights;
            _rawMetars = rawMetars;
        }

        /// <summary>
        /// Сохранение датасета
        /// </summary>
        public void SaveDataSet()
        {
            Logging.Instance().LogNotification("Запуск процесса сохранения данных");
            saveFlightDataFile(StringConstants.DeparturesFile, DirectionType.Departure);
            saveFlightDataFile(StringConstants.ArrivalsFile, DirectionType.Arrival);
            saveMetarDataFile();
            Logging.Instance().LogNotification("Данные успешно сохранены");
        }


        /// <summary>
        /// Запись данных в файл
        /// </summary>
        /// <param name="filePath">Путь до файла</param>
        /// <param name="direction">Направление</param>
        private void saveFlightDataFile(string filePath, DirectionType direction)
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

        /// <summary>
        /// Сохранение нераспарсенного METAR
        /// ToDo: Реализовать парсер для будущего разбора датасета
        /// </summary>
        private void saveMetarDataFile()
        {
            var directory = Path.GetDirectoryName(StringConstants.MetarsFile);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            using var writer = new StreamWriter(StringConstants.MetarsFile, true);
            foreach (var metar in _rawMetars)
            {
                writer.WriteLine(metar);
            }
        }
    }
}

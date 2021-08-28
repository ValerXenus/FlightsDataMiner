using System;
using FlightsDataMiner.Common;

namespace FlightsDataMiner.Objects
{
    /// <summary>
    /// Класс сведений о полете
    /// </summary>
    public class FlightInfo
    {
        /// <summary>
        /// Номер рейса
        /// </summary>
        public string FlightNumber { get; set; }

        /// <summary>
        /// Пункт назначения
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// Тип воздушного судна
        /// </summary>
        public PlaneTypes PlaneType { get; set; }

        /// <summary>
        /// Авиакомпания
        /// </summary>
        public Airlines Airline { get; set; }

        /// <summary>
        /// Тип направления (Вылет/Прилет)
        /// </summary>
        public DirectionType DirectionType { get; set; }

        /// <summary>
        /// Дата и время (вылета/прибытия) по расписанию
        /// </summary>
        public DateTime ScheduledDateTime { get; set; }

        /// <summary>
        /// Дата и время (вылета/прибытия) реальное
        /// </summary>
        public DateTime ActualDateTime { get; set; }
    }
}

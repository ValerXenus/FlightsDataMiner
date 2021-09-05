using System;
using FlightsDataMiner.Base.Common.Enums;

namespace FlightsDataMiner.Base.Objects
{
    /// <summary>
    /// Класс сведений о полете
    /// </summary>
    public record FlightInfo
    {
        /// <summary>
        /// Номер рейса
        /// </summary>
        public string FlightNumber { get; init; }

        /// <summary>
        /// Пункт назначения
        /// </summary>
        public string Destination { get; init; }

        /// <summary>
        /// Тип воздушного судна
        /// </summary>
        public PlaneTypes PlaneType { get; init; }

        /// <summary>
        /// Авиакомпания
        /// </summary>
        public Airlines Airline { get; init; }

        /// <summary>
        /// Тип направления (Вылет/Прилет)
        /// </summary>
        public DirectionType DirectionType { get; init; }

        /// <summary>
        /// Дата и время (вылета/прибытия) по расписанию
        /// </summary>
        public DateTime ScheduledDateTime { get; set; }

        /// <summary>
        /// Дата и время (вылета/прибытия) реальное
        /// </summary>
        public DateTime ActualDateTime { get; set; }

        /// <summary>
        /// Проверка на заполненность всех данных
        /// </summary>
        /// <returns></returns>
        public bool CheckDataValid()
        {
            return ScheduledDateTime != DateTime.MinValue && ActualDateTime != DateTime.MinValue &&
                   !string.IsNullOrEmpty(FlightNumber) && !string.IsNullOrEmpty(Destination) &&
                   Airline != Airlines.None && PlaneType != PlaneTypes.None;
        }
    }
}

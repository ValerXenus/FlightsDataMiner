using System;

namespace FlightsDataMiner.Base.Common
{
    /// <summary>
    /// Класс строковых констант
    /// </summary>
    public class StringConstants
    {
        /// <summary>
        /// Путь до HTML-файла со списком вылетющих рейсов
        /// </summary>
        public static string DeparturesFilename = "DeparturesRaw.html";

        /// <summary>
        /// Путь до HTML-файла со списком прилетющих рейсов
        /// </summary>
        public static string ArrivalsFilename = "ArrivalsRaw.html";

        /// <summary>
        /// Ссылка на данные METAR
        /// </summary>
        public static string MetarDataUrl = "https://api.met.no/weatherapi/tafmetar/1.0/metar.txt?date={request_date}&offset=+03:00&icao=UWKD";

        /// <summary>
        /// Ссылка на страницу со списком вылетающих рейсов
        /// </summary>
        public static string DeparturesUrl = "https://kazan.aero/on-line-schedule/";

        /// <summary>
        /// Ссылка на страницу со списком прилетающих рейсов
        /// </summary>
        public static string ArrivalsUrl = "https://kazan.aero/on-line-schedule/arrival/";

        /// <summary>
        /// Ссылка на файл вылетающих рейсов
        /// </summary>
        public static string DeparturesFile = string.Concat(AppContext.BaseDirectory, "DataSet\\Departures.txt");

        /// <summary>
        /// Ссылка на файл прибывающих рейсов
        /// </summary>
        public static string ArrivalsFile = string.Concat(AppContext.BaseDirectory, "DataSet\\Arrivals.txt");

        /// <summary>
        /// Ссылка на файла с погодными данными
        /// </summary>
        public static string MetarsFile = string.Concat(AppContext.BaseDirectory, "DataSet\\RawMetars.txt");

        /// <summary>
        /// XPath до элементов таблицы "Вылет"
        /// </summary>
        public static string FlightsXPath =
            "//body/div[@class='cont']/div[2]/div[@class='main-content__template']/div[@class='flights-shedule']/div[@class='shedule__items']/div";

        /// <summary>
        /// Путь до таблицы с данными о рейсе внутри узла рейсов сайта
        /// </summary>
        public static string FlightTableXPath = "div[@class='shedule__item-table']/div/";

        /// <summary>
        /// XPath до поля "Статус рейса"
        /// </summary>
        public static string FlightStatusXPath = string.Concat(FlightTableXPath, "div[@class='shedule__item-cell shedule_status']");

        /// <summary>
        /// XPath до поля "Номер рейса"
        /// </summary>
        public static string FlightNumberXPath = string.Concat(FlightTableXPath, "div[@class='shedule__item-cell shedule_number']/div/span");

        /// <summary>
        /// XPath до поля "Название авиакомпании"
        /// </summary>
        public static string AirlineXPath = string.Concat(FlightTableXPath, "div[@class='shedule__item-cell shedule_number']/div");

        /// <summary>
        /// XPath до поля "Город назначения"
        /// </summary>
        public static string DestinationXPath = string.Concat(FlightTableXPath, "div[@class='shedule__item-cell shedule_direction']");

        /// <summary>
        /// XPath узла с датами
        /// </summary>
        public static string FlightDateTimeXPath = string.Concat(FlightTableXPath, "div[@class='shedule__item-cell shedule_time']");

        /// <summary>
        /// XPath даты/времени рейса, совпадающего с расписанием
        /// </summary>
        public static string CoincidentDateTimeXPath = "div[@class='time_currect']";

        /// <summary>
        /// XPath даты/времени рейса по расписанию
        /// </summary>
        public static string ScheduledDateTimeXPath = "div[@class='time_old']";

        /// <summary>
        /// XPath реального даты/времени рейса
        /// </summary>
        public static string ActualDateTimeXPath = "div[@class='time_new']";

        // <summary>
        // XPath до поля "Модели самолета"
        // </summary>
        //public static string PlaneNameXPath = "td[6]";
    }
}

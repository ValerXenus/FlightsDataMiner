namespace FlightsDataMiner.Common
{
    /// <summary>
    /// Класс строковых констант
    /// </summary>
    public class StringConstants
    {
        /// <summary>
        /// Ссылка на страницу с онлайн-табло
        /// </summary>
        public static string FlightsDashboardUrl = "http://kazan.aero/for-passengers-and-guests/on-line-schedule/";

        /// <summary>
        /// Ссылка на данные METAR
        /// </summary>
        public static string MetarDataUrl = "https://api.met.no/weatherapi/tafmetar/1.0/metar.txt?date={request_date}&icao=UWKD";

        /// <summary>
        /// Ссылка на файл вылетающих рейсов
        /// </summary>
        public static string DeparturesFile = "DataSet\\Departures.txt";

        /// <summary>
        /// Ссылка на файл прибывающих рейсов
        /// </summary>
        public static string ArrivalsFile = "DataSet\\Arrivals.txt";

        /// <summary>
        /// Ссылка на файла с погодными данными
        /// </summary>
        public static string MetarsFile = "DataSet\\RawMetars.txt";

        /// <summary>
        /// XPath до элементов таблицы "Вылет"
        /// </summary>
        public static string DepartureFlightsXPath =
            "//body/div[@class='wrapper']/div[@id='content']/div[@id='workarea']/div[@class='tabs']/div[@id='first']/table/tr[@class='vilet_r']";

        /// <summary>
        /// XPath до элементов таблицы "Прилет"
        /// </summary>
        public static string ArrivalFlightsXPath =
            "//body/div[@class='wrapper']/div[@id='content']/div[@id='workarea']/div[@class='tabs']/div[@id='second']/table/tr[@class='prilet_r']";

        /// <summary>
        /// XPath до поля "Статус рейса"
        /// </summary>
        public static string FlightStatusXPath = "td[2]";

        /// <summary>
        /// XPath до поля "Номер рейса"
        /// </summary>
        public static string FlightNumberXPath = "td[3]";

        /// <summary>
        /// XPath до поля "Номер рейса"
        /// </summary>
        public static string AirlineXPath = "td[5]/a[@class='ac_logo']/img";

        /// <summary>
        /// XPath даты вылета
        /// </summary>
        public static string DepartureDateXPath = "td[8]";

        /// <summary>
        /// XPath даты прилета
        /// </summary>
        public static string ArrivalDateXPath = "td[9]";

        /// <summary>
        /// XPath до поля "Модели самолета"
        /// </summary>
        public static string PlaneNameXPath = "td[6]";

        /// <summary>
        /// XPath до поля "Город назначения"
        /// </summary>
        public static string DestinationXPath = "td[4]";

        /// <summary>
        /// XPath до поля "Время по расписанию" (вылета)
        /// </summary>
        public static string ScheduledDepartureTimeXPath = "td[7]/text()";

        /// <summary>
        /// XPath до поля "Время по расписанию" (прилета)
        /// </summary>
        public static string ScheduledArrivalTimeXPath = "td[8]/text()";

        /// <summary>
        /// XPath до поля "Реальное время" (вылетаа)
        /// </summary>
        public static string ActualTimeXPath = "td[@class='tot']/text()[2]";
    }
}

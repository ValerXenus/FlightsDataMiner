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
        /// XPath до элементов таблицы "Вылет"
        /// </summary>
        public static string DepartureFlightsXPath =
            "//body/div[@class='wrapper']/div[@id='content']/div[@id='workarea']/div[@class='tabs']/div[@id='first']/table/tr[@class='vilet_r']";

        /// <summary>
        /// XPath до элементов таблицы "Прилет"
        /// </summary>
        public static string ArrivalFlightsXPath =
            "//body/div[@class='wrapper']/div[@id='content']/div[@id='workarea']/div[@class='tabs']/div[@id='second']/table/tr[@class='prilet_r']";
    }
}

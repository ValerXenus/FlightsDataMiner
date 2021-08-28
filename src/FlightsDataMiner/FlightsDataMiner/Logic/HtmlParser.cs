using FlightsDataMiner.Common;
using HtmlAgilityPack;

namespace FlightsDataMiner.Logic
{
    /// <summary>
    /// Класс для парсинга HTML
    /// </summary>
    public class HtmlParser
    {
        /// <summary>
        /// Поле объекта HTML-документа
        /// </summary>
        private readonly HtmlDocument _htmlDocument;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="htmlContent">HTML содержимое в виде строки</param>
        public HtmlParser(string htmlContent)
        {
            _htmlDocument = new HtmlDocument();
            _htmlDocument.LoadHtml(htmlContent);
        }

        public void GetDepartureFlights()
        {
            var nodes = _htmlDocument.DocumentNode.SelectNodes(StringConstants.DepartureFlightsXPath);
        }
    }
}

using FlightsDataMiner.Base.Common;
using FlightsDataMiner.Logic.Logging;

namespace FlightsDataMiner.Data
{
    /// <summary>
    /// Получение данных из сайта аэропорта
    /// </summary>
    public class FlightsDataAccess
    {
        /// <summary>
        /// Получение HTML-контента онлайн-табло
        /// </summary>
        /// <returns></returns>
        public string GetDashboardHtml()
        {
            Logging.Instance().LogNotification("Загрузка данных о рейсах");
            
            var requestService = new RequestService(StringConstants.FlightsDashboardUrl);
            return requestService.SendRequest();
        }
    }
}

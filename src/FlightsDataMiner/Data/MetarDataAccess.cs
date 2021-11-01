using System.Collections.Generic;
using System.Linq;
using FlightsDataMiner.Base.Common;
using FlightsDataMiner.Logic;
using FlightsDataMiner.Logic.Logging;

namespace FlightsDataMiner.Data
{
    /// <summary>
    /// Получение данных METAR
    /// </summary>
    public class MetarDataAccess
    {
        /// <summary>
        /// Путь URL для получения данных о погоде
        /// </summary>
        private readonly string _metarUrl;

        public MetarDataAccess(AppSettings settings)
        {
            _metarUrl = StringConstants.MetarDataUrl.Replace("{request_date}", settings.CurrentDate.ToString("yyyy-MM-dd"));
        }

        public List<string> GetMetarData()
        {
            Logging.Instance().LogNotification("Загрузка данных о погоде");

            var requestService = new RequestService(_metarUrl);
            return requestService.SendRequest(true).Split("|")
                .Where(x => !string.IsNullOrEmpty(x))
                .ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using FlightsDataMiner.Base.Common;
using FlightsDataMiner.Logic.Logging;

namespace FlightsDataMiner.Data
{
    /// <summary>
    /// Получение данных METAR
    /// </summary>
    public class MetarDataAccess
    {
        private readonly string _metarUrl;

        public MetarDataAccess()
        {
            // Данные по Metar представляются по UTC
            var nowDateText = DateTime.UtcNow.ToString("yyyy-MM-dd");
            _metarUrl = StringConstants.MetarDataUrl.Replace("{request_date}", nowDateText);
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

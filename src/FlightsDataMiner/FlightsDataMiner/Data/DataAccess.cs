using System;
using System.IO;
using System.Net;
using System.Text;
using FlightsDataMiner.Common;
using FlightsDataMiner.Logic.Logging;

namespace FlightsDataMiner.Data
{
    /// <summary>
    /// Получение данных из сайта аэропорта
    /// </summary>
    public class DataAccess
    {
        public DataAccess()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public string GetDashboardHtml()
        {
            Logging.Instance().LogNotification("Загрузка данных о рейсах");
            var outcome = "";

            Logging.Instance()
                .LogNotification($"Отправка запроса на получение списка рейсов: {StringConstants.FlightsDashboardUrl}");
            var request = WebRequest.Create(StringConstants.FlightsDashboardUrl);
            using var response = request.GetResponse();
            Logging.Instance().LogNotification("Ответ успешно получен");
            using var stream = response.GetResponseStream();
            if (stream == null)
                return string.Empty;

            using var reader = new StreamReader(stream, Encoding.GetEncoding(1251));

            string line;
            while ((line = reader.ReadLine()) != null)
                outcome += line;

            Logging.Instance().LogNotification("Ответ успешно прочитан");
            return outcome;
        }
    }
}

using System.IO;
using System.Net;
using System.Text;
using FlightsDataMiner.Logic.Logging;

namespace FlightsDataMiner.Data
{
    /// <summary>
    /// Класс для отправки запросов
    /// </summary>
    public class RequestService
    {
        private readonly string _requestUrl;

        public RequestService(string requestUrl)
        {
            _requestUrl = requestUrl;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        /// <summary>
        /// Отпрака HTTP запроса
        /// </summary>
        /// <param name="needSeparator">Признак необходимости сепаратора между блоками (строками) данных</param>
        /// <returns></returns>
        public string SendRequest(bool needSeparator = false)
        {
            var outcome = "";

            Logging.Instance()
                .LogNotification($"Отправка запроса на получение списка рейсов: {_requestUrl}");

            var request = WebRequest.Create(_requestUrl);
            using var response = request.GetResponse();
            Logging.Instance().LogNotification("Ответ успешно получен");

            using var stream = response.GetResponseStream();
            if (stream == null)
                return string.Empty;

            using var reader = new StreamReader(stream, Encoding.GetEncoding(1251));
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var separator = needSeparator
                    ? "|"
                    : "";
                outcome += line + separator;
            }

            Logging.Instance().LogNotification("Ответ успешно прочитан");
            return outcome;
        }
    }
}

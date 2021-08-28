using System.IO;
using System.Net;
using System.Text;
using FlightsDataMiner.Common;

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
            var outcome = "";

            var request = WebRequest.Create(StringConstants.FlightsDashboardUrl);
            using var response = request.GetResponse();
            using var stream = response.GetResponseStream();
            if (stream == null)
                return string.Empty;

            using var reader = new StreamReader(stream, Encoding.GetEncoding(1251));

            string line;
            while ((line = reader.ReadLine()) != null)
                outcome += line;

            return outcome;
        }
    }
}

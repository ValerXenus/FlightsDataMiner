using System;
using System.Threading.Tasks;
using FlightsDataMiner.Logic;
using PuppeteerSharp;

namespace FlightsDataMiner.Data
{
    /// <summary>
    /// Класс браузера без рендеринга Web-страниц
    /// </summary>
    internal class HeadlessBrowser : IDisposable
    {
        /// <summary>
        /// Браузер
        /// </summary>
        private Browser _browser;

        public HeadlessBrowser(AppSettings settings)
        {
            var initTask = initBrowser(settings.ChromePath);
            initTask.Wait();
        }

        /// <summary>
        /// Синхронная обертка над асинхронным getHtmlPageContentAsync
        /// </summary>
        /// <param name="pageUrl">URL страницы</param>
        /// <param name="jsFunctionContent">Содержимое JS функции, которое необходимо выполнить
        /// после загрузки страницы</param>
        /// <returns></returns>
        public string GetHtmlContent(string pageUrl, string jsFunctionContent = "")
        {
            var getHtmlTask = getHtmlPageContentAsync(pageUrl, jsFunctionContent);
            getHtmlTask.Wait();

            return getHtmlTask.Result;
        }

        /// <summary>
        /// Получить HTML-контент страницы
        /// </summary>
        /// <param name="pageUrl">URL страницы</param>
        /// <param name="jsFunctionContent">Содержимое JS функции, которое необходимо выполнить
        /// после загрузки страницы</param>
        /// <returns></returns>
        private async Task<string> getHtmlPageContentAsync(string pageUrl, string jsFunctionContent)
        {
            var page = await _browser.NewPageAsync();
            await page.GoToAsync(pageUrl);
            if (!string.IsNullOrEmpty(jsFunctionContent))
            {
                await page.EvaluateFunctionAsync($"() => {{ {jsFunctionContent} }}");
            }

            return await page.GetContentAsync();
        }

        /// <summary>
        /// Инициализировать браузер
        /// <param name="chromePath">Ссылка на .exe-файл браузера Chrome</param>
        /// </summary>
        private async Task initBrowser(string chromePath)
        {
            _browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                ExecutablePath = chromePath
            });
        }

        /// <summary>
        /// Закрытие браузера
        /// </summary>
        public void Dispose()
        {
            var disposeTask = _browser.CloseAsync();
            disposeTask.Wait();
        }
    }
}

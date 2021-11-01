namespace FlightsDataMiner.Base.Common.Enums
{
    /// <summary>
    /// Режим чтения авиарейсов
    /// </summary>
    public enum FlightsReadMode
    {
        /// <summary>
        /// Не определен
        /// </summary>
        None = 0,

        /// <summary>
        /// Файл вылетов
        /// </summary>
        Departures = 1, 

        /// <summary>
        /// Файл прилетов
        /// </summary>
        Arrivals = 2
    }
}

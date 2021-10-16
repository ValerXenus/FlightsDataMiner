namespace FlightsDataMiner.Data
{
    /// <summary>
    /// Режим чтения файла
    /// </summary>
    public enum FileReadMode
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

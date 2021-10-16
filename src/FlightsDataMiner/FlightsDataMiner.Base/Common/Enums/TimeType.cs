namespace FlightsDataMiner.Base.Common.Enums
{
    /// <summary>
    /// Тип даты и времени
    /// </summary>
    public enum TimeType
    {
        /// <summary>
        /// Не определен
        /// </summary>
        None = 0,

        /// <summary>
        /// Дата и время по расписанию
        /// </summary>
        Scheduled = 1,

        /// <summary>
        /// Актуальная дата и время
        /// </summary>
        Actual = 2
    }
}

using System.ComponentModel;

namespace FlightsDataMiner.Common
{
    /// <summary>
    /// Типы самолетов
    /// </summary>
    public enum PlaneTypes
    {
        [Description("Неизвестный")]
        None = 0,
        /// <summary>
        /// Airbus A319
        /// </summary>
        [Description("A319")]
        A319 = 1,
        /// <summary>
        /// Airbus A320
        /// </summary>
        [Description("A320")]
        A320 = 2,
        /// <summary>
        /// Airbus A321
        /// </summary>
        [Description("A321")]
        A321 = 3,
        /// <summary>
        /// Airbus A330-200
        /// </summary>
        [Description("A332")] 
        A332 = 4,

        /// <summary>
        /// АН-26
        /// </summary>
        [Description("AN26")] 
        AN26 = 5,

        /// <summary>
        /// ATR-72
        /// </summary>
        [Description("AT72")] 
        AT72 = 6,
        /// <summary>
        /// ATR-75
        /// </summary>
        [Description("AT75")] 
        AT75 = 7,

        /// <summary>
        /// Boeing 737-500
        /// </summary>
        [Description("B735")] 
        B735 = 8,
        /// <summary>
        /// Boeing 737-800
        /// </summary>
        [Description("B738")] 
        B738 = 9,
        /// <summary>
        /// Boeing 757-200
        /// </summary>
        [Description("B752")] 
        B752 = 10,
        /// <summary>
        /// Boeing 767-300
        /// </summary>
        [Description("B763")] 
        B763 = 11,

        /// <summary>
        /// Bombardier CRJ-100
        /// </summary>
        [Description("CRJ1")] 
        CRJ1 = 12,
        /// <summary>
        /// Bombardier CRJ-200
        /// </summary>
        [Description("CRJ2")] 
        CRJ2 = 13,

        /// <summary>
        /// Embraer E170
        /// </summary>
        [Description("E170")] 
        E170 = 14,
        /// <summary>
        /// Embraer E190
        /// </summary>
        [Description("E190")] 
        E190 = 15,

        /// <summary>
        /// L-410
        /// </summary>
        [Description("L410")] 
        L410 = 16,

        /// <summary>
        /// Sukhoi Superjet 100-95
        /// </summary>
        [Description("SU95")] 
        SU95 = 17,

        /// <summary>
        /// ЯК-42
        /// </summary>
        [Description("YK42")] 
        YK42 = 18,
    }
}

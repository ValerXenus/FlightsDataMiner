using System.ComponentModel;

namespace FlightsDataMiner.Base.Common.Enums
{
    /// <summary>
    /// Авиакомпании
    /// </summary>
    public enum Airlines
    {
        [Description("Неизвестный")]
        None = 0,
        [Description("ПАО АЭРОФЛОТ")]
        Aeroflot = 1,
        [Description("ПОБЕДА")]
        Pobeda = 2,
        [Description("УРАЛЬСКИЕ АВИАЛИНИИ")]
        UralAirlines = 3,
        [Description("АО АВИАКОМПАНИЯ РОССИЯ")]
        Rossiya = 4,
        [Description("АЗУР ЭЙР")]
        AzurAir = 5,
        [Description("АО АВИАКОМПАНИЯ СИБИРЬ")]
        S7 = 6,
        [Description("РОЯЛ ФЛАЙ")]
        RoyalFly = 7,
        [Description("АО ЮВТ АЭРО")]
        UvtAero = 8,
        [Description("АЭРОПОРТ ОРЕНБУРГ")]
        Orenburg = 9,
        [Description("СЕВЕРНЫЙ ВЕТЕР")]
        Nordwind = 10,
        [Description("КОСТРОМСКОЕ АВИАПРЕДПРИЯТИЕ")]
        Kostroma = 11,
        [Description("СМАРТАВИА")]
        SmartAvia = 12,
        [Description("ПАО АВИАКОМПАНИЯ ЮТЭЙР")]
        UTair = 13,
        [Description("РУСЛАЙН")]
        Rusline = 14,
        [Description("ИКАР")]
        Ikar = 15,
        [Description("АО АВИАКОМПАНИЯ АЗИМУТ")]
        Azimut = 16,
        [Description("УЗБЕКИСТОН ХАВО ЙУЛЛАРИ")]
        UzbekistanHavoUllary = 17,
        [Description("ИЖАВИА")]
        IzhAvia = 18,
        [Description("ФЛАЙДУБАЙ")]
        FlyDubai = 19,
        [Description("ТУРЕЦКИЕ АВИАЛИНИИ")]
        TurkishAirlines = 20,
        [Description("ЯМАЛ")]
        Yamal = 21,
        [Description("РЕД ВИНГС")]
        RedWings = 22,
    }
}

using System;
using System.ComponentModel;
using System.Linq;

namespace FlightsDataMiner.Logic
{
    /// <summary>
    /// Класс для работы с данными enum
    /// </summary>
    public class EnumTranslator
    {
        /// <summary>
        /// Получение описания из enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDescription(Enum value)
        {
            var info = value.GetType().GetField(value.ToString());

            if (info?.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Any())
                return attributes.First().Description;

            return value.ToString();
        }

        /// <summary>
        /// Получение значения enum по описанию
        /// </summary>
        /// <typeparam name="T">Тип enum</typeparam>
        /// <param name="description">Описание, по которому возвращается значение enum</param>
        /// <returns></returns>
        public static T GetValueFromDescription<T>(string description) where T : Enum
        {
            foreach (var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            return default;
        }
    }
}

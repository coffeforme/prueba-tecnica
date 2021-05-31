using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Web.Mvc;
using System.Linq;
using System.Text;
using System.Reflection;

namespace core.util
{
    public static class extends
    {
        public static SelectList GetList<T>(List<T> data, string value, string text)
        {
            return new SelectList(data, value, text);
        }
        public const string DateTimeFormat = "dd/MM/yyyy HH:mm";
        public const string DateFormat = "dd/MM/yyyy";
        public const string DateTimeFormat2 = "MM-dd-yyyy H:mm tt";
        public const string DateTimeFormat3 = "yyyy-MM-dd H:mm";
        public static DateTime? ConvertDate(this string date)
        {
            return string.IsNullOrEmpty(date) ? (DateTime?)null : DateTime.Parse(date);
        }
        public static DateTime? ConvertDateTime(this string date)
        {
            return string.IsNullOrEmpty(date) ? (DateTime?)null : DateTime.ParseExact(date, DateTimeFormat, CultureInfo.CurrentUICulture);
        }

        public static DateTime? ConvertDateTime2(this string date)
        {
            return string.IsNullOrEmpty(date) ? (DateTime?)null : DateTime.ParseExact(date, DateTimeFormat2, CultureInfo.CurrentUICulture);
        }

        public static DateTime? ConvertDateTime3(this string date)
        {
            return string.IsNullOrEmpty(date) ? (DateTime?)null : DateTime.ParseExact(date, DateTimeFormat3, CultureInfo.CurrentUICulture);
        }

        public static decimal? ConvertMoney(this string money)
        {
            return string.IsNullOrEmpty(money) ? (decimal?)null : Convert.ToDecimal(money.Replace("$ ", "").Replace(".", ""));
        }

        public static string ConvertDate(this DateTime date)
        {
            return date.ToString(DateFormat);
        }

        public static string ConvertDateTime(this DateTime date)
        {
            return date.ToString(DateTimeFormat);
        }

        public static string ConvertDateTime2(this DateTime date)
        {
            return date.ToString(DateTimeFormat2);
        }

        public static string RemoverAcentos(this string s)
        {
            Encoding destEncoding = Encoding.GetEncoding("iso-8859-8");

            return destEncoding.GetString(
             Encoding.Convert(Encoding.UTF8, destEncoding, Encoding.UTF8.GetBytes(s)));
        }




        /// <summary>
        /// Retorna el valor tipado de una clave en el appSetting
        /// </summary>
        /// <typeparam name="T">Tipo de dato esperado</typeparam>
        /// <param name="AppSettingKey">Nombre de la appSetting</param>
        /// <returns></returns>
        public static T GetValueAppSetting<T>(this string AppSettingKey)
        {
            if (!ConfigurationManager.AppSettings.AllKeys.Any(k => k.Equals(AppSettingKey, StringComparison.CurrentCultureIgnoreCase)))
            {
                Log.Write($"No se encontro la clave {AppSettingKey} dentro del AppSetting");
                return default;
            }
            string value = ConfigurationManager.AppSettings[AppSettingKey].ToString();
            if (string.IsNullOrEmpty(value))
            {

                Log.Write($"El valor para la clave {AppSettingKey} es nulo o vacio");
                return default;
            }
            else //if (ob is T)
            {
                try
                {
                    return (T)Convert.ChangeType(value, typeof(T));
                }
                catch
                {

                    Log.Write($"El valor '{value}' para la clave no corresponde con el tipo requerido de la clave {AppSettingKey}");
                    return default;
                }
            }
        }


        public static void MatchAndMap<TSource, TDestination>(this TSource source, TDestination destination)
            where TSource : class, new()
            where TDestination : class, new()
        {
            if (source != null && destination != null)
            {
                List<PropertyInfo> sourceProperties = source.GetType().GetProperties().ToList<PropertyInfo>();
                List<PropertyInfo> destinationProperties = destination.GetType().GetProperties().ToList<PropertyInfo>();

                foreach (PropertyInfo sourceProperty in sourceProperties)
                {
                    PropertyInfo destinationProperty = destinationProperties.Find(item => item.Name == sourceProperty.Name);

                    if (destinationProperty != null)
                    {
                        try
                        {
                            destinationProperty.SetValue(destination, sourceProperty.GetValue(source, null), null);
                        }
                        catch (Exception ex)
                        {
                            Log.Write(ex);
                        }
                    }
                }
            }

        }

        public static TDestination MapProperties<TDestination>(this object source)
            where TDestination : class, new()
        {
            var destination = Activator.CreateInstance<TDestination>();
            MatchAndMap(source, destination);

            return destination;
        }

        public static int ToInt<T>(this T soure) where T : IConvertible//enum
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            return (int)(IConvertible)soure;
        }

        //ShawnFeatherly funtion (above answer) but as extention method
        public static int Count<T>(this T soure) where T : IConvertible//enum
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            return Enum.GetNames(typeof(T)).Length;
        }
    }
}

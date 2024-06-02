using System;
using System.Globalization;

public static class DateTimeExtensions
{
    public static DateTime ConvertToDifferentFormat(this DateTime originalDateTime, string originalFormat, string targetFormat)
    {
        // Convertir la fecha al formato especificado
        string dateString = originalDateTime.ToString(originalFormat);

        // Parsear la cadena en un nuevo objeto DateTime con el formato deseado
        DateTime convertedDateTime = DateTime.ParseExact(dateString, originalFormat, CultureInfo.InvariantCulture);

        return convertedDateTime;
    }
}

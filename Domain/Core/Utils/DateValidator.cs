

using System.Globalization;

namespace Domain.Core.Utils
{
    public static class DateValidator
    {
        public static bool TryParseAndValidateDate(string dateString, out DateTime parsedDate)
        {
            // Intenta parsear la fecha usando formatos comunes y CultureInfo.InvariantCulture
            // InvariantCulture es crucial para evitar problemas con configuraciones regionales
            // que interpretan "MM/dd/yyyy" o "dd/MM/yyyy" de forma diferente.
            // Se pueden especificar múltiples formatos.

            // Formatos ISO 8601 son los más recomendados por su universalidad.
            // "o" es el formato de ida y vuelta (round-trip) que incluye milisegundos y zona horaria.
            // "s" es el formato de fecha y hora ordenable (YYYY-MM-DDTHH:mm:ss).
            string[] formats = {
            "yyyy-MM-ddTHH:mm:ss.fffZ", // ISO 8601 con milisegundos y Z para UTC
            "yyyy-MM-ddTHH:mm:ssZ",    // ISO 8601 sin milisegundos y Z para UTC
            "yyyy-MM-dd HH:mm:ss",     // Formato común sin zona horaria
            "yyyy-MM-dd",              // Solo fecha
            "M/d/yyyy",                // Formato MM/DD/YYYY
            "d/M/yyyy",                // Formato DD/MM/YYYY
            "MM-dd-yyyy",
            "dd-MM-yyyy"
        };

            if (DateTime.TryParseExact(dateString, formats, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out parsedDate))
            {
                // La fecha se parseó correctamente y se ajustó a UTC si tenía información de zona horaria.
                // Si la fecha original no tenía información de zona horaria, DateTimeKind será Unspecified.
                // Es buena práctica asegurarse de que sea UTC para la base de datos.
                if (parsedDate.Kind == DateTimeKind.Unspecified)
                {
                    // Si es Unspecified, asume que es una fecha local y conviértela a UTC.
                    // Considera si este es el comportamiento deseado.
                    // En muchos casos, las fechas de entrada deberían tener una zona horaria definida.
                    parsedDate = parsedDate.ToUniversalTime();
                }
                return true;
            }
            else
            {
                throw new Exception("No se pudo cambiar el formato de la fecha");
            }
        }
    }
}

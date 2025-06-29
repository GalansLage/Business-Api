

using System.Globalization;

namespace Domain.Core.Utils
{
    public static class ToTitleCase
    {
        public static string TitleCase(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return input;
            }

            // Usamos CultureInfo para manejar correctamente las reglas de mayúsculas/minúsculas de un idioma.
            // CultureInfo.InvariantCulture es una opción segura y neutral.
            var textInfo = CultureInfo.InvariantCulture.TextInfo;

            // Convertimos todo a minúsculas primero para manejar casos como "LAPTOP GAMER" -> "Laptop Gamer"
            return textInfo.ToTitleCase(input.ToLower());
        }
    }
}

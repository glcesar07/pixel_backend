using System.Globalization;
using System.Text;

namespace Application.Functions
{
    public class CleanText
    {
        public static string RemoveSpaces(string value)
        {
            value = value.Replace(" ", string.Empty);
            return value.Trim();
        }

        public static string RemoveAccents(string text)
        {
            StringBuilder sbReturn = new ();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return sbReturn.ToString();
        }

        public static string AddGuion(string value)
        {
            value = value.Replace(" ", "_");
            return value.Trim();
        }
    }
}

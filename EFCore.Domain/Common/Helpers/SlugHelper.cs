using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace EFCore.Domain.Common.Helpers;

public static class SlugHelper
{
    public static string Generate(string phrase)
    {
        string str = RemoveAccent(phrase).ToLower();

        str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
        str = Regex.Replace(str,@"\s+","-").Trim();
        str = str.Substring(0, str.Length <= 45 ? str.Length:45).Trim();
        str = Regex.Replace(str, @"^-+|-+$", "");


        return str.Replace("-", "_");

    }

    public static string RemoveAccent(string phrase)
    {
        string normalizedString = phrase.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach(char c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if(unicodeCategory is not UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);

    }
    
}

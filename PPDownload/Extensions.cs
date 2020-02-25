namespace PPDownload
{
    internal static class Extensions
    {
        internal static decimal? ParseDecimalNullable(this string str)
        {
            if (decimal.TryParse(str, out decimal d))
            {
                return d;
            }

            return null;
        }

        internal static int? ParseIntNullable(this string str)
        {
            if (int.TryParse(str, out int i))
            {
                return i;
            }

            return null;
        }

        internal static string CleanInnerHtml(this string str) =>
            str.Replace("\\N", "").Replace("\\n", "").Replace("\n", "").TrimStart().TrimEnd();
    }
}
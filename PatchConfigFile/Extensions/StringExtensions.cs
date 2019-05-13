namespace PatchConfigFile.Extensions
{
    /// <summary>
    ///  StringExtensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Fulls the trim.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string FullTrim(this string value)
        {
            return value.Trim('\r', '\n', '\t', ' ');
        } 
    }
}

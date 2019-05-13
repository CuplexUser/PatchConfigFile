namespace PatchConfigFile.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class PatchParameters
    {
        /// <summary>
        /// Gets or sets a value indicating whether [insert mode].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [insert mode]; otherwise, <c>false</c>.
        /// </value>
        public bool ReplaceMode { get; set; }

        /// <summary>
        /// Gets or sets the name of the initialize file.
        /// </summary>
        /// <value>
        /// The name of the initialize file.
        /// </value>
        public string InitFileName { get; set; }

        /// <summary>
        /// Gets or sets the search string.
        /// </summary>
        /// <value>
        /// The search string.
        /// </value>
        public string[] SearchStrings { get; set; }

        /// <summary>
        /// Gets or sets the insert string.
        /// </summary>
        /// <value>
        /// The insert string.
        /// </value>
        public string[] InsertStrings { get; set; }

        /// <summary>
        /// Gets or sets the complete configuration file.
        /// </summary>
        /// <value>
        /// The complete configuration file.
        /// </value>
        public string[] CompleteConfigFile { get;set; }
    }
}
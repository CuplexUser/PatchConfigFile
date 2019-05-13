using System.Collections.Generic;

namespace PatchConfigFile.GeneralParsers
{
   /// <summary>
   /// 
   /// </summary>
   public interface IParseTextConfFile
   {
      /// <summary>
      /// Gets the row lookup table.
      /// </summary>
      /// <value>
      /// The row lookup table.
      /// </value>
      IDictionary<int, string> RowLookupTable { get; }

      /// <summary>
      /// Opens the configuration file.
      /// </summary>
      /// <param name="fileName">Name of the file.</param>
      /// <param name="rowLookup">The row lookup.</param>
      /// <returns></returns>
      bool ParseConfigFile(string fileName, out IDictionary<int, string> rowLookup);

      // Internal storage
      /// <summary>
      /// Opens the configuration file.
      /// </summary>
      /// <param name="fileName">Name of the file.</param>
      /// <returns></returns>
      bool ParseConfigFile(string fileName);
   }

}

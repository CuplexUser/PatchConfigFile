using PatchConfigFile.GeneralParsers;
using System.Collections.Generic;

namespace PatchConfigFile.ParseMatcher
{
   /// <summary>
   /// 
   /// </summary>
   public class MatchCollection
   {
      /// <summary>
      /// Gets or sets a value indicating whether [configuration data already patched].
      /// </summary>
      /// <value>
      ///   <c>true</c> if [configuration data already patched]; otherwise, <c>false</c>.
      /// </value>
      public bool ConfigDataAlreadyPatched { get; set; }

      /// <summary>
      /// Gets the maxed rows.
      /// </summary>
      /// <value>
      /// The maxed rows.
      /// </value>
      public IDictionary<RowAttribute, string> RowsMatched { get; private set; }

      /// <summary>
      /// Gets or sets a value indicating whether [full match].
      /// </summary>
      /// <value>
      ///   <c>true</c> if [full match]; otherwise, <c>false</c>.
      /// </value>
      public bool FullMatch { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether this instance is matched.
      /// </summary>
      /// <value>
      ///   <c>true</c> if this instance is matched; otherwise, <c>false</c>.
      /// </value>
      public bool IsMatched { get; set; }
   }
}
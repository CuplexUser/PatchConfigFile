using System;
using System.Collections;
using System.Collections.Generic;
using PatchConfigFile.ParseMatcher;

namespace PatchConfigFile.GeneralParsers
{
   /// <summary>
   /// 
   /// </summary>
   public interface IParseMatcher
   {
      /// <summary>
      /// Gets the configuration rows.
      /// </summary>
      /// <value>
      /// The configuration rows.
      /// </value>
      IDictionary<RowAttribute, string> ConfigRows { get; }

      /// <summary>
      /// Gets the search rows.
      /// </summary>
      /// <value>
      /// The search rows.
      /// </value>
      IDictionary<RowAttribute, string> SearchRows { get; }

      /// <summary>
      /// Gets the replace rows.
      /// </summary>
      /// <value>
      /// The replace rows.
      /// </value>
      IDictionary<RowAttribute, string> ReplaceRows { get; }

      /// <summary>
      /// Gets a value indicating whether this instance is parsed.
      /// </summary>
      /// <value>
      ///   <c>true</c> if this instance is parsed; otherwise, <c>false</c>.
      /// </value>
      bool IsParsed { get; }
   }
}
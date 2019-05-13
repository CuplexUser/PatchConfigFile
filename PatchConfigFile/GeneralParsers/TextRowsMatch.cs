using System.Collections.Generic;

namespace PatchConfigFile.GeneralParsers
{
   public class TextRowsMatch : IParseMatcher
   {
      public IDictionary<RowAttribute, string> ConfigRows { get; set; }

      public IDictionary<RowAttribute, string> SearchRows { get; set; }
      public IDictionary<RowAttribute, string> ReplaceRows { get; set; }

      public bool IsMatch { get; set; }

      public bool IsAlreadyParsed { get; set; }

      public bool GetMatches<TMatchCollection>()
      {
         throw new System.NotImplementedException();
      }

      public bool IsParsed { get; set; }
   }
}
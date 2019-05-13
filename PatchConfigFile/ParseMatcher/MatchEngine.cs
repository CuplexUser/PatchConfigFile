using PatchConfigFile.Extensions;
using PatchConfigFile.GeneralParsers;
using System.Collections.Generic;

namespace PatchConfigFile.ParseMatcher
{
   public class PatchMatchEngine
   {
      private TextRowsMatch _rowsMatches;

      public MatchCollection GetTextMatchResult(IDictionary<int, string> parsedTextRows, IDictionary<int, string> searchStringsRows, IDictionary<int, string> replaceStringsRows)
      {
         ConvertMatchData(parsedTextRows, searchStringsRows, replaceStringsRows);
         var mc = new MatchCollection();

         return mc;
      }

      private void ConvertMatchData(IDictionary<int, string> parsedTextRows, IDictionary<int, string> searchStringsRows, IDictionary<int, string> replaceStringsRows)
      {
         IDictionary<RowAttribute, string> configRows = new Dictionary<RowAttribute, string>();
         IDictionary<RowAttribute, string> searchRows = new Dictionary<RowAttribute, string>();
         IDictionary<RowAttribute, string> replaceRows = new Dictionary<RowAttribute, string>();
         _rowsMatches = new TextRowsMatch();

         foreach (int key in parsedTextRows.Keys)
         {
            string rowVal = parsedTextRows[key];
            RowAttribute rowAttr = new RowAttribute { RowNumber = key, RowText = rowVal.FullTrim() };
            rowAttr.RowTrimmedLength = rowAttr.RowText.Length;
            configRows.Add(rowAttr, rowAttr.RowText);
         }

      }
   }
}
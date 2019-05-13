using PatchConfigFile.Extensions;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PatchConfigFile.GeneralParsers
{
   public class ParseTextBlockFile : IParseTextConfFile
   {
      public IDictionary<int, string> RowLookupTable { get; private set; }

      public bool ParseConfigFile(string fileName, out IDictionary<int, string> rowLookup)
      {
         rowLookup= new Dictionary<int, string>();
         try
         {
            var fileStream = File.OpenRead(fileName);
            var textReader = new StreamReader(fileStream, Encoding.UTF8);
            
            int index = 0;

            while (!textReader.EndOfStream)
            {
               string line = textReader.ReadLine();
               line = line.FullTrim();
               rowLookup.Add(index++, line);
            }
         }
         catch (Exception exception)
         {
            Log.Error(exception, "LoadParseFileException");
            return false;
         }

         return true;
      }

      public bool ParseConfigFile(string fileName)
      {
         bool result= ParseConfigFile(fileName, out IDictionary<int, string> lookup);

         if (result)
         {
            RowLookupTable = lookup;
         }

         return result;
      }
   }
}
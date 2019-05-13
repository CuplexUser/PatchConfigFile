using PatchConfigFile.Extensions;
using PatchConfigFile.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PatchConfigFile
{
    /// <summary>
    ///  ConfigFilePatcher - Core module for find and edit
    /// </summary>
    public class ConfigFilePatcher
    {
        /// <summary>
        /// Patches the configuration file.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public bool PatchConfigFile(PatchParameters config, out string message)
        {
            FileStream fs = null;
            MinMaxSearchResult matchResult = new MinMaxSearchResult();
            List<string> outputConfigData= new List<string>();

            int searchStringIndex = config.SearchStrings.Length - 1;
            if (searchStringIndex < 0)
            {
                searchStringIndex = 0;
            }

            try
            {
                string[] allConfigLines = File.ReadAllLines(config.InitFileName, Encoding.UTF8);
                string searchString = config.SearchStrings[searchStringIndex].FullTrim();

                for (int i = config.SearchStrings.Length; i < allConfigLines.Length; i++)
                {
                    if (allConfigLines[i].Contains(searchString))
                    {
                        // Compare arrays
                        if (CompareArrays(allConfigLines, config.SearchStrings, i))
                        {
                            matchResult.MatchedAllRows = true;
                            matchResult.StartIndex = i - config.SearchStrings.Length + 1;
                            matchResult.EndIndex = matchResult.StartIndex + config.SearchStrings.Length - 1;
                            break;
                        }
                    }
                }

                if (!matchResult.MatchedAllRows)
                {
                    message = "Could find SearchString(s), config file left unedited";
                    return false;
                }

                fs = File.Create(config.InitFileName);
                TextWriter textWriter = new StreamWriter(fs, Encoding.UTF8);
                
                if (config.InsertStrings == null)
                {
                    message = "InsertStrings can not be null!";
                    return false;
                }

                // Append after
                if (config.ReplaceMode)
                {
                    // All config lines before search hit 
                    outputConfigData.AddRange(allConfigLines.Take(matchResult.EndIndex));

                    int postStartIndex = outputConfigData.Count;

                    // Insert config
                    config.InsertStrings[0] += "\n";
                    outputConfigData.AddRange(config.InsertStrings);

                    

                    // Add everything missing after inserted text.
                    outputConfigData.AddRange(allConfigLines.Skip(matchResult.EndIndex));
                }
                else
                {
                    outputConfigData.AddRange(allConfigLines.Take(matchResult.StartIndex));
                    outputConfigData.AddRange(config.InsertStrings);
                    outputConfigData.AddRange(allConfigLines.Skip(matchResult.StartIndex));
                }

                message = "Successfully patched the file by inserting the configured rows";
                foreach (string configLine in outputConfigData)
                {
                    textWriter.WriteLine(configLine);
                }
               
                textWriter.Flush();
                fs.Flush(true);
                textWriter.Close();
            }
            catch (Exception exception)
            {
                message = exception.Message;
                return false;
            }
            finally
            {
                fs?.Close();
            }

            message = "Ok";
            return true;


        }

        /// <summary>
        /// Compares the arrays.
        /// </summary>
        /// <param name="allConfigLines">All configuration lines.</param>
        /// <param name="configSearchStrings">The configuration search strings.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        private static bool CompareArrays(string[] allConfigLines, IReadOnlyList<string> configSearchStrings, int index)
        {
            int stopIndex = index;
            int matches = 0;
            int searchIndex = index - configSearchStrings.Count + 1;

            for (int i = searchIndex; i < stopIndex; i++)
            {
                if (allConfigLines[i].Contains(configSearchStrings[matches]))
                {
                    matches++;
                }
            }



            return matches == configSearchStrings.Count - 1;
        }
        public bool CheckIfPatchAlreadyApplied(PatchParameters config)
        {


            return false;
            //MinMaxSearchResult matchResult= new MinMaxSearchResult();
            //int searchStringIndex = config.SearchStrings.Length - 1;
            //if (searchStringIndex < 0)
            //{
            //    searchStringIndex = 0;
            //}

            //string[] allConfigLines = File.ReadAllLines(config.InitFileName, Encoding.UTF8);
            //string searchString = config.SearchStrings[searchStringIndex].FullTrim();

            //for (int i = config.SearchStrings.Length; i < allConfigLines.Length; i++)
            //{
            //    if (allConfigLines[i].Contains(searchString))
            //    {
            //        // Compare arrays
            //        if (CompareArrays(allConfigLines, config.SearchStrings, i))
            //        {
            //            matchResult.MatchedAllRows = true;
            //            matchResult.StartIndex = i - config.SearchStrings.Length + 1;
            //            matchResult.EndIndex = matchResult.StartIndex + config.SearchStrings.Length - 1;
            //            break;
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        struct MinMaxSearchResult
        {
            /// <summary>
            /// The index
            /// </summary>
            public int StartIndex;
            /// <summary>
            /// The length
            /// </summary>
            public int EndIndex;
            /// <summary>
            /// The matched all rows
            /// </summary>
            public bool MatchedAllRows;
        }
    }
}
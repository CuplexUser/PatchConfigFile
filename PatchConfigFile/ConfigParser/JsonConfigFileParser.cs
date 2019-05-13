using Newtonsoft.Json;
using PatchConfigFile.Models;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace PatchConfigFile.ConfigParser
{
    public class JsonConfigFileParser : ConfigFileParserBase
    {
        private JsonConfigFileParser() { }

        public override PatchParameters ParseConfigFile(string filename)
        {
            PatchParameters patchParams = null;
            try
            {
                string jsonContent = File.ReadAllText(filename);
                patchParams = JsonConvert.DeserializeObject<PatchParameters>(jsonContent);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Failed to parse json config file");
                Console.WriteLine(exception.Message);
            }

            return patchParams;
        }

        public override bool ValidateParameters(PatchParameters config, out string message)
        {
            if (string.IsNullOrEmpty(config.InitFileName) || !File.Exists(config.InitFileName))
            {
                message = $"Config file is not valid: {config.InitFileName}";
                return false;
            }

            int searchLength = config.SearchStrings?.Sum(x => x.Length) ?? 0;
            if (config.SearchStrings != null && (config.SearchStrings.Length == 0 || searchLength < 15))
            {
                var sb = new StringBuilder();
                sb.AppendLine("Invalid SearchStrings config, must be atl east 15 characters");
                sb.AppendLine("from config file:");

                if (config.SearchStrings != null)
                {
                    foreach (string searchString in config.SearchStrings)
                    {
                        sb.AppendLine(searchString);
                    }
                }

                message = sb.ToString();
            }


            int insertLength = config.SearchStrings?.Sum(x => x.Length) ?? 0;
            if (config.InsertStrings != null && (config.InsertStrings.Length == 0 || insertLength < 5))
            {
                var sb = new StringBuilder();
                sb.AppendLine("Invalid InsertStrings config, must be atl east 5 characters");
                sb.AppendLine("from config file:");

                if (config.InsertStrings != null)
                {
                    foreach (string insertString in config.InsertStrings)
                    {
                        sb.AppendLine(insertString);
                    }
                }

                message = sb.ToString();
            }

            message = "Ok";
            return true;
        }

        public new static IConfigFileParser CreateInstance()
        {
            return new JsonConfigFileParser();
        }
    }
}
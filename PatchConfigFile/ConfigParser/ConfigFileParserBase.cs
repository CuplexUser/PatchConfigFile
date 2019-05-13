using System;
using PatchConfigFile.Models;

namespace PatchConfigFile.ConfigParser
{
    public abstract class ConfigFileParserBase: IConfigFileParser
    {
        public abstract PatchParameters ParseConfigFile(string filename);

        public abstract bool ValidateParameters(PatchParameters config, out string message);

        public static IConfigFileParser CreateInstance;
    }
}

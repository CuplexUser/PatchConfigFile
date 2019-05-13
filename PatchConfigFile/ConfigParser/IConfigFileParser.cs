using PatchConfigFile.Models;

namespace PatchConfigFile.ConfigParser
{
    public interface IConfigFileParser
    {
        PatchParameters ParseConfigFile(string filename);

        bool ValidateParameters(PatchParameters config, out string message);
    }
}
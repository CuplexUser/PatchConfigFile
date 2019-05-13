using System;
using System.Text.RegularExpressions;

namespace PatchConfigFile.Models
{
    public static class HelpModule
    {
        public static bool MaxesHelpArgument(string argument)
        {
            Regex pattern= new Regex(@"^(/h|/help|/\?|-h|-help|-\?)");
            return pattern.IsMatch(argument);
        }

        public static void DisplayHelpText()
        {
            Console.WriteLine("");
            Console.WriteLine("Replaces or append config in specified file after passing the path to the config file like. *.json file");
            Console.WriteLine("");
            Console.WriteLine("Example: PatchConfigFile PatchSettingsEvars.json");
            Console.WriteLine(@"Example: %Localappdata%\Localscriptfiles\Patchconfigfile.exe PatchSettingsEvars.json");
            Console.WriteLine("So only one input parameter is allowed pointing to the patchConfig file");
            Console.WriteLine("-------------------------------------------------------------------------------------------");
            Console.WriteLine("---------------------------------Made by Martin Dahl 2019----------------------------------");
            Console.WriteLine("-------------------------------------------------------------------------------------------");
        }
    }
}
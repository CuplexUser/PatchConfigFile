using PatchConfigFile.ConfigParser;
using PatchConfigFile.Models;
using System;
using System.IO;
using System.Text;

namespace PatchConfigFile
{
    class Program
    {
        static void Main(string[] args)
        {
            var sb = new StringBuilder();
            if (args.Length == 1 && HelpModule.MaxesHelpArgument(args[0]))
            {
                HelpModule.DisplayHelpText();
                return;
            }

            if (args.Length != 1)
            {
                Console.WriteLine("Incorrect number of parameters, expected settings.json");
            }
            else
            {
                string confFilePath = args[0];
                bool patchSuccessful = false;

                if (!string.IsNullOrEmpty(confFilePath) && confFilePath.EndsWith(".json", StringComparison.CurrentCultureIgnoreCase))
                {
                    try
                    {
                        if (!File.Exists(confFilePath))
                        {
                            Console.WriteLine($"The file {confFilePath} does not exist!");
                            return;
                        }

                        var confFileParser = JsonConfigFileParser.CreateInstance();
                        var config = confFileParser.ParseConfigFile(confFilePath);

                        if (config == null)
                        {
                            Console.WriteLine("Patch was aborted");
                            return;
                        }

                        bool validParameters = confFileParser.ValidateParameters(config, out var message);
                        if (!validParameters)
                        {
                            Console.WriteLine($"Invalid config, problem: {message}");
                            return;
                        }

                        

                        if (patcher.CheckIfPatchAlreadyApplied(config))
                        {
                            Console.WriteLine($"The Patch has already been applied. Aborting");
                            return;
                        }

                        patchSuccessful = patcher.PatchConfigFile(config, out var errorMessage);
                        if (!patchSuccessful)
                        {
                            sb.AppendLine(errorMessage);
                        }
                    }
                    catch (Exception exception)
                    {
                        sb.AppendLine(exception.Message);
                    }

                    if (patchSuccessful)
                    {
                        Console.WriteLine("Patch was successful");
                    }
                    else
                    {
                        Console.WriteLine("Patch failed!");
                        Console.WriteLine(sb.ToString());
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect parameter format, expected [*.json] like settings.json");
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using PatchConfigFile.Models;

namespace PatchConfigFile.ConfigParser
{
    [Obsolete("Please use the JsonConfig file parser instead")]
    public sealed class XmlInputConfigParser: IConfigFileParser
    {
        public PatchParameters ParseConfigFile(string filename)
        {
            PatchParameters parameters = new PatchParameters();
            try
            {
                XmlDocument xmlDoc = new XmlDocument(); // Create an XML document object
                xmlDoc.Load(filename);

                XmlElement configElement = xmlDoc.DocumentElement;
                if (configElement != null)
                {
                    parameters.ReplaceMode = bool.Parse(configElement.Attributes["AppendMode"].Value);
                    XmlNodeList configElements = configElement.ChildNodes;

                    Dictionary<string, string> configDictionary = new Dictionary<string, string>();

                    for (int i = 0; i < configElements.Count; i++)
                    {
                        XmlNode node = configElements[i];
                        configDictionary.Add(node.Name, node.InnerText);
                    }


                    parameters.InitFileName = configDictionary["InitFileName"];
                    parameters.SearchStrings = configDictionary["SearchString"].Split("\r\n".ToCharArray());
                    parameters.InsertStrings = configDictionary["InsertString"].Split("\r\n".ToCharArray());
                }
                else
                {
                    Console.WriteLine("configElement was not found!");
                    return null;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Config File parse exception");
                Console.WriteLine(exception.Message);
                return null;
            }


            return parameters;
        }

        bool IConfigFileParser.ValidateParameters(PatchParameters config, out string message)
        {
            string uncPath = PathFinder.ResolveToRootUnc(config.InitFileName);
            Console.WriteLine($"UNC path for {config.InitFileName} is : {uncPath}");

            if (!File.Exists(config.InitFileName))
            {
                message = $"Invalid InitFileName, file: {config.InitFileName} does not exist";
                return false;
            }

            if (config.SearchStrings.Length <= 1 && config.SearchStrings[0].Length < 15)
            {
                message = $"Invalid search string length, must be at lest 15 characters. configured string is: {config.SearchStrings[0].Length} characters long";
                return false;
            }

            if (config.InsertStrings.Length <= 1 && config.InsertStrings[0].Length < 15)
            {
                message = $"Invalid insert string length, must be at lest 5 characters. configured string is: {config.InsertStrings[0].Length} characters long";
                return false;
            }

            message = "Ok";
            return true;
        }

        public IConfigFileParser CreateInstance()
        {
            return new XmlInputConfigParser();
        }
    }
}
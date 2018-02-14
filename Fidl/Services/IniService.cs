namespace Fidl.Services
{
    using System.IO;

    using Fidl.Services.Interfaces;

    using IniParser;
    using IniParser.Model;
    using IniParser.Model.Configuration;
    using IniParser.Parser;

    internal class IniService : IIniService
    {
        private static readonly FileIniDataParser IniParser = new FileIniDataParser(new IniDataParser(new IniParserConfiguration { SkipInvalidLines = true }));

        public void AddKey(string filename, string section, string key, string value)
        {
            if (!File.Exists(filename))
            {
                File.WriteAllText(filename, string.Empty);
            }

            IniData iniData = IniParser.ReadFile(filename);

            if (!iniData.Sections.ContainsSection(section))
            {
                iniData.Sections.AddSection(section);
            }

            KeyDataCollection autorunSection = iniData.Sections[section];

            if (!autorunSection.ContainsKey(key))
            {
                autorunSection.AddKey(key);
            }

            autorunSection.GetKeyData(key).Value = value;

            IniParser.WriteFile(filename, iniData);
        }

        public void RemoveKey(string filename, string section, string key)
        {
            if (!File.Exists(filename)) return;

            IniData iniData = IniParser.ReadFile(filename);

            if (iniData.Sections.ContainsSection(section))
            {
                KeyDataCollection autorunSection = iniData.Sections[section];

                if (autorunSection.ContainsKey(key))
                {
                    autorunSection.RemoveKey(key);
                }

                if (autorunSection.Count == 0)
                {
                    iniData.Sections.RemoveSection(section);
                }

                IniParser.WriteFile(filename, iniData);
            }

            if (File.ReadAllText(filename) == string.Empty)
            {
                File.Delete(filename);
            }
        }
    }
}
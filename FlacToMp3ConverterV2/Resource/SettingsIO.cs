using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlacToMp3ConverterV2.Model;
using FlacToMp3ConverterV2.Properties;

namespace FlacToMp3ConverterV2.Resource
{
    internal static class SettingsIO
    {
        internal static void SaveSettings(Dictionary<string, string> settings)
        {
            Settings defaultSettings = Settings.Default;
            defaultSettings.inputPath = settings["inputPath"];
            defaultSettings.outputPath = settings["outputPath"];
            defaultSettings.ffmpegPath = settings["ffmpegPath"];

            defaultSettings.Save();
        }
    }
}

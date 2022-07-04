using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlacToMp3ConverterV2.Model;

namespace FlacToMp3ConverterV2.IO
{
    internal static class FlacToMP3ConverterFileIO
    {
        internal static List<Dictionary<string, string>> GetFlacFiles(string inputPath)
        {
            List<Dictionary<string, string>> flacFiles = new List<Dictionary<string, string>>();

            foreach (string file in Directory.GetFiles(inputPath, "*.flac", SearchOption.AllDirectories))
            {
                Dictionary<string, string> flacFile = new Dictionary<string, string>();
                flacFile["name"] = Path.GetFileName(file.Replace(".flac", ""));
                flacFile["path"] = file;
                flacFiles.Add(flacFile);
            }

            return flacFiles;
        }

        internal static void CreateMP3Files(List<MP3File> mp3files, string ffmpegPath, ModelService modelService)
        {
            List<Task> tasks = new List<Task>();

            foreach (MP3File mp3file in mp3files)
            {
                Task task = Task.Factory.StartNew(() =>
                {
                    Process ffmpegProcess = Process.Start(ffmpegPath, $"-i \"{Path.GetFullPath(mp3file.GetFlacFile().Path)}\" -y \"{Path.GetFullPath(mp3file.Path)}\"");
                    ffmpegProcess.WaitForExit();
                });

                task.GetAwaiter().OnCompleted(() => TaskFinished(modelService, tasks));
                tasks.Add(task);
            }
        }

        internal static void TaskFinished(ModelService modelService, List<Task> tasks)
        {
            bool allTasksFinished = true;
            foreach (Task task in tasks)
            {
                if (!task.IsCompleted)
                {
                    allTasksFinished = false;
                }
            }

            if (allTasksFinished)
            {
                modelService.TasksFinished();
            }
        }
    }
}

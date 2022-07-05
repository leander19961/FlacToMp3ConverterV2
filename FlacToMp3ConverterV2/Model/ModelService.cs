using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FlacToMp3ConverterV2.View.FlacToMP3ConverterViewer;

namespace FlacToMp3ConverterV2.Model
{
    public class ModelService
    {
        private FlacToMP3ConverterView mainWindow;

        private List<FlacFile> flacFiles;
        private List<MP3File> mp3Files;

        private FlacToMP3ConverterPath inputPath;
        private FlacToMP3ConverterPath outputPath;
        private FlacToMP3ConverterPath ffmpegPath;

        public FlacToMP3ConverterPath InputPath
        {
            get { return inputPath; }
        }
        public FlacToMP3ConverterPath OutputPath
        {
            get { return outputPath; }
        }
        public FlacToMP3ConverterPath FFmpegPath
        {
            get { return ffmpegPath; }
        }

        public ModelService(FlacToMP3ConverterView mainWindow, string inputPath, string outputPath, string ffmpegPath)
        {
            this.mainWindow = mainWindow;

            this.inputPath = new FlacToMP3ConverterPath("inputPath", inputPath);
            this.outputPath = new FlacToMP3ConverterPath("outputPath", outputPath);
            this.ffmpegPath = new FlacToMP3ConverterPath("ffmpegPath", ffmpegPath);

            flacFiles = new List<FlacFile>();
            mp3Files = new List<MP3File>();
        }

        public void ChangePath(FlacToMP3ConverterPath flacToMP3ConverterPath, string newPath)
        {
            flacToMP3ConverterPath.Path = newPath.Replace("\"", "");
        }

        public List<FlacFile> GetFlacFiles()
        {
            return flacFiles;
        }

        public List<MP3File> GetMP3Files()
        {
            return mp3Files;
        }

        public void CreateFlacFile(string name, string path)
        {
            FlacFile flacFile = new FlacFile(name, path);

            flacFiles.Add(flacFile);
        }

        public void CreateFlacFiles(List<Dictionary<string, string>> files)
        {
            foreach (Dictionary<string, string> file in files)
            {
                string name = file["name"];
                string path = file["path"];

                if (name != null && path != null)
                {
                    CreateFlacFile(name, path);
                }
            }
        }

        public void CreateMP3File(string name, string path, FlacFile flacFile)
        {
            MP3File mp3File = new MP3File(name, path);

            mp3File.setFlacFile(flacFile);

            mp3Files.Add(mp3File);
        }

        public void CreateMP3Files(List<FlacFile> files)
        {
            foreach (FlacFile flacFile in files)
            {
                string fileName = $"{Path.GetFileName(flacFile.Name)}";
                string fileDirectory = Path.GetDirectoryName(flacFile.Path);
                string interpretDirectory = Path.GetDirectoryName(fileDirectory);
                fileDirectory = fileDirectory.Substring(fileDirectory.LastIndexOf('\\'));
                interpretDirectory = interpretDirectory.Substring(interpretDirectory.LastIndexOf("\\"));
                string outputDirectory = $"{outputPath.Path}{interpretDirectory}{fileDirectory}";
                string filePath = $"{outputDirectory}\\{fileName}.mp3";

                if (!Directory.Exists(outputDirectory))
                {
                    Directory.CreateDirectory(outputDirectory);
                }

                if (fileName != null && filePath != null)
                {
                    CreateMP3File(fileName, filePath, flacFile);
                }
            }
        }

        public void TasksFinished()
        {
            mainWindow.AllTasksFinished();
        }
    }
}

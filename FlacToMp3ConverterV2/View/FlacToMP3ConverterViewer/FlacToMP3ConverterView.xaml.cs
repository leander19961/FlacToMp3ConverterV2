using FlacToMp3ConverterV2.IO;
using FlacToMp3ConverterV2.Model;
using FlacToMp3ConverterV2.Properties;
using FlacToMp3ConverterV2.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static FlacToMp3ConverterV2.IO.FlacToMP3ConverterFileIO;

namespace FlacToMp3ConverterV2.View.FlacToMP3ConverterViewer
{
    /// <summary>
    /// Interaktionslogik für FlacToMP3ConverterView.xaml
    /// </summary>
    public partial class FlacToMP3ConverterView : Window
    {
        private ModelService modelService;

        public FlacToMP3ConverterView()
        {
            InitializeComponent();

            modelService = new ModelService(this, Settings.Default.inputPath, Settings.Default.outputPath, Settings.Default.ffmpegPath);
        }

        private void ButtonGetFlacFiles_Click(object sender, RoutedEventArgs e)
        {
            ListViewFlac.Items.Clear();

            modelService.CreateFlacFiles(GetFlacFiles(modelService.InputPath.Path));

            foreach (FlacFile flacFile in modelService.GetFlacFiles())
            {
                ListViewFlac.Items.Add(flacFile);
            }

            ListViewFlac.Items.Refresh();
        }

        private void ButtonCreateMP3Files_Click(object sender, RoutedEventArgs e)
        {
            ListViewMP3.Items.Clear();

            modelService.CreateMP3Files(modelService.GetFlacFiles());

            foreach (MP3File mp3File in modelService.GetMP3Files())
            {
                ListViewMP3.Items.Add(mp3File);
            }

            ListViewMP3.Items.Refresh();
        }

        private void ButtonOpenFoldersView_Click(object sender, RoutedEventArgs e)
        {
            ViewManager.ShowFoldersView(this, modelService);
        }

        private void ButtonStartConverting_Click(object sender, RoutedEventArgs e)
        {
            ProgressBarTaskWork.IsIndeterminate = true;
            CreateMP3Files(modelService.GetMP3Files(), modelService.FFmpegPath.Path, modelService);
        }

        public void AllTasksFinished()
        {
            ProgressBarTaskWork.IsIndeterminate = false;
        }

        private void Application_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Dictionary<string, string> settings = new Dictionary<string, string>();
            settings["inputPath"] = modelService.InputPath.Path;
            settings["outputPath"] = modelService.OutputPath.Path;
            settings["ffmpegPath"] = modelService.FFmpegPath.Path;
            SettingsIO.SaveSettings(settings);
        }
    }
}

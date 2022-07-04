using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlacToMp3ConverterV2.Model
{
    public class MP3File
    {
        private string name;
        private string path;

        private FlacFile flacFile;

        public string Name
        {
            get { return name; }
        }
        public string Path
        {
            get { return path; }
        }

        public MP3File(string name, string path)
        {
            this.name = name;
            this.path = path;
        }

        public FlacFile GetFlacFile()
        {
            return flacFile;
        }

        public MP3File setFlacFile(FlacFile value)
        {
            if (this.flacFile == null)
            {
                flacFile = value;
                flacFile.setMP3File(this);
            }

            return this;
        }
    }
}

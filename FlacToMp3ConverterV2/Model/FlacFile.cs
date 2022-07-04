using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlacToMp3ConverterV2.Model
{
    public class FlacFile
    {
        private string name;
        private string path;

        private MP3File mp3File;

        public string Name
        {
            get { return name; }
        }
        public string Path
        {
            get { return path; }
        }

        public FlacFile(string name, string path)
        {
            this.name = name;
            this.path = path;
        }

        public MP3File GetMP3File()
        {
            return mp3File;
        }

        public FlacFile setMP3File(MP3File value)
        {
            if (this.mp3File == null)
            {
                mp3File = value;
                mp3File.setFlacFile(this);
            }

            return this;
        }
    }
}

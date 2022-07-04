using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlacToMp3ConverterV2.Model
{
    public class FlacToMP3ConverterPath
    {
        private string name;
        private string path;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public FlacToMP3ConverterPath(string name, string path)
        {
            this.name = name;
            this.path = path;
        }
    }
}

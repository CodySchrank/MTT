using System.Collections.Generic;
using System.IO;

namespace MTT
{
    public class ModelFile
    {
        public string Name { get; set; }

        public string[] Info { get; set; } 

        public List<LineObject> Objects { get; set; }

        public string Structure { get; set; }

        public ModelFile()
        {
            Objects = new List<LineObject>();
        }
    }
}
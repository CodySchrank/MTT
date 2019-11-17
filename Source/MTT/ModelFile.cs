using System.Collections.Generic;
using System.IO;

namespace MTT
{
    public class ModelFile
    {
        public string Name { get; set; }

        public string[] Info { get; set; } 

        public string Inherits { get; set; }

        public string InheritenceStructure { get; set; }

        public bool IsEnum { get; set; }

        public List<LineObject> Objects { get; set; }

        public List<EnumObject> EnumObjects { get; set; }

        public string Structure { get; set; }

        public ModelFile() {
            Objects = new List<LineObject>();
            EnumObjects = new List<EnumObject>();
        }

        public string PrintBasic() {
            return Name.ToString()
            + " : /" + Structure.ToString();
        }

        public override string ToString() {
            var str = "" + this.PrintBasic() + ":\n";

            if(Objects != null) {
                foreach (var obj in Objects) {
                    str += obj.VariableName + ": " + obj.Type + "\n";
                }
            }

            return str + "\n";
        }
    }
}
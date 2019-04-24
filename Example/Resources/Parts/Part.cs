using Example.Resources.Parts.Unit;

namespace Example.Resources.Parts
{
    public class Part: Entity
    {
        public string PartName { get; set; }

        public string PartCategory { get; set; }

        public string Routenumber { get; set; }  // because it contains the word enum

        public Units Units { get; set; }
    }   
}
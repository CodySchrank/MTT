using Example.Resources.Parts.Unit;

namespace Example.Resources.Parts
{
    public class Part: Entity
    {
        public Part(
            string partName,
            string partCategory,
            string routeNumber,
            Units units)
       {
            PartName = partName;
            PartCategory = partCategory;
            Routenumber = routeNumber;
            Units = units;
       }

        public string PartName { get; set; }

        public string PartCategory { get; set; }

        public string Routenumber { get; set; }  // because it contains the word enum

        public Units Units { get; set; }
    }   
}
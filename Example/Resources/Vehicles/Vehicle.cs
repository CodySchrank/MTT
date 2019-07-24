using System.Collections.Generic;
using Example.Resources.Parts;

//empty preprocessor directive with conflicting keyword
#if enum
#endif

namespace Example.Resources.Vehicles
{
    public abstract class Vehicle : Entity 
    {
        // this is a top level comment
        public int Year { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public int? Mileage;

        public Dictionary<string, string> keyValuePair { get; set; }

        public VehicleState Condition { get; set; }  // this is an enum of type int

        public virtual ICollection<Part> Parts { get; set; }
    }
}
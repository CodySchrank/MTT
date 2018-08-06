using System.Collections.Generic;
using Example.Resources.Parts;

namespace Example.Resources.Vehicles
{
    public class Vehicle : Entity 
    {
        // this is a top level comment
        public int Year { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public int? Mileage { get; set; }

        public VehicleState Condition { get; set; }  // this is an enum of type int

        public virtual ICollection<Part> Parts { get; set; }
    }
}
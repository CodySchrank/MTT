using System.Collections.Generic;
using Example.Resources.Parts;

namespace Example.Resources.Vehicles
{
    public class Vehicle : Entity 
    {
        // this is a top level comment
        public VehicleMake Make { get; set; }

        public VehicleModel Model { get; set; }

        public VehicleYear Year { get; set; }

        public VehicleState Condition { get; set; }  // this is an enum of type int

        public string Description { get; set; }

        public ICollection<VehiclePart> Parts { get; set; }
    }
}
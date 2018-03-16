using System.Collections.Generic;
using Example.Resources.Parts;

namespace Example.Resources.Vehicles
{
    public class Vehicle : Entity
    {
        public VehicleMake Make { get; set; }

        public VehicleModel Model { get; set; }

        public VehicleYear Year { get; set; }

        public ICollection<VehiclePart> Parts { get; set; }
    }
}
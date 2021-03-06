using System;
using System.Collections.Generic;
using Example.Resources.Parts;
using Example.Resources.Parts.Unit;

//empty preprocessor directive with conflicting keyword
#if enum
#endif

namespace Example.Resources.Vehicles
{
    public abstract class Vehicle : Entity 
    {
        public Vehicle(int year, string make, string model) 
        {
            Year = year;
            Make = make;
            model = model;
        }
        
        // this is a top level comment
        public int Year { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public int? Mileage;

        public Dictionary<string, Units> Options { get; set; }

        public VehicleState Condition { get; set; }  // this is an enum of type int

        public virtual ICollection<Part> Parts { get; set; }
        
        public IList<Part> SpareParts { get; set; } = new List<Part>();

        public Guid UserId { get; set; } 

        public ReadOnlyDictionary<string, IList<Units>> UnitsListMap { get; set; }
    }
}
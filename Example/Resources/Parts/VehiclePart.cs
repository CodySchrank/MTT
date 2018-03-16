namespace Example.Resources.Parts
{
    public class VehiclePart : KeyValuePair
    {
        public int QuantityAvailable { get; set; }

        public PartCategory PartCategory { get; set; }
    }   
}
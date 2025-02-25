namespace VesselPowerManagement.Models
{
    public class Vessel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public class PowerCalculation
    {
        public int Id { get; set;}
        public int VesselId { get; set; }
        public decimal EnginePower { get; set; }
        public decimal FuelConsumption { get; set; }
        public decimal Speed { get; set; }
    }
}
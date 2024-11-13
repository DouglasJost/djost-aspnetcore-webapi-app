using BusStopSimulation.Models.Enums;

namespace BusStopSimulation.Entities
{
    public class Passenger
    {
        public string Name { get; set; } = string.Empty;
        public DestinationType Destination {  get; set; }
        
        public Passenger(string name, DestinationType destination)
        {
            this.Name = name;
            this.Destination = destination;
        }

        public override string ToString()
        {
            return $"{Name} to {Destination.ToString()}";
        }
    }
}

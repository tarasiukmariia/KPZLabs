using System;
using System.Collections.Generic;

namespace DesignPatterns.Mediator
{
    class CommandCentre
    {
        private List<Runway> _runways = new List<Runway>();
        private List<Aircraft> _aircrafts = new List<Aircraft>();

        public CommandCentre()
        {
        }

        public void RegisterRunway(Runway runway)
        {
            _runways.Add(runway);
        }

        public void RegisterAircraft(Aircraft aircraft)
        {
            _aircrafts.Add(aircraft);
        }

        public bool CanLand(Aircraft aircraft)
        {
            foreach (var runway in _runways)
            {
                if (runway.IsAvailable())
                {
                    return true;
                }
            }
            return false;
        }

        public void Land(Aircraft aircraft)
        {
            foreach (var runway in _runways)
            {
                if (runway.IsAvailable())
                {
                    aircraft.Land(runway);
                    return;
                }
            }
            Console.WriteLine($"Could not land aircraft {aircraft.Name}, no available runways.");
        }

        public void TakeOff(Aircraft aircraft)
        {
            foreach (var runway in _runways)
            {
                if (runway.IsBusyWithAircraft == aircraft)
                {
                    aircraft.TakeOff(runway);
                    return;
                }
            }
            Console.WriteLine($"Could not take off aircraft {aircraft.Name}, not found on any runway.");
        }
    }

    class Aircraft
    {
        public string Name;

        public Aircraft(string name)
        {
            this.Name = name;
        }

        public void Land(Runway runway)
        {
            Console.WriteLine($"Aircraft {this.Name} is landing.");
            Console.WriteLine($"Checking runway.");
            runway.AssignAircraft(this);
            Console.WriteLine($"Aircraft {this.Name} has landed.");
        }

        public void TakeOff(Runway runway)
        {
            Console.WriteLine($"Aircraft {this.Name} is taking off.");
            runway.ReleaseAircraft(this);
            Console.WriteLine($"Aircraft {this.Name} has taken off.");
        }
    }

    class Runway
    {
        public readonly Guid Id = Guid.NewGuid();
        public Aircraft? IsBusyWithAircraft;

        public bool IsAvailable()
        {
            return IsBusyWithAircraft == null;
        }

        public void AssignAircraft(Aircraft aircraft)
        {
            if (IsAvailable())
            {
                IsBusyWithAircraft = aircraft;
                Console.WriteLine($"Aircraft {aircraft.Name} has been assigned to runway {this.Id}.");
            }
            else
            {
                Console.WriteLine($"Could not land, the runway {this.Id} is busy.");
            }
        }

        public void ReleaseAircraft(Aircraft aircraft)
        {
            if (IsBusyWithAircraft == aircraft)
            {
                IsBusyWithAircraft = null;
                Console.WriteLine($"Runway {this.Id} is now free.");
            }
            else
            {
                Console.WriteLine($"Aircraft {aircraft.Name} is not assigned to this runway {this.Id}.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CommandCentre commandCentre = new CommandCentre();

            Runway runway1 = new Runway();
            Runway runway2 = new Runway();

            commandCentre.RegisterRunway(runway1);
            commandCentre.RegisterRunway(runway2);

            Aircraft aircraft1 = new Aircraft("Boeing 747");
            Aircraft aircraft2 = new Aircraft("Airbus A320");

            commandCentre.RegisterAircraft(aircraft1);
            commandCentre.RegisterAircraft(aircraft2);

            commandCentre.Land(aircraft1);
            commandCentre.Land(aircraft2);
            commandCentre.TakeOff(aircraft1);
            commandCentre.TakeOff(aircraft2);
        }
    }
}

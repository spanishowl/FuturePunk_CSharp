using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuturePunk
{
    abstract class Vehicle
    {
        public abstract int Fuel { get; protected set; }
        public abstract int Speed { get; protected set; }
        public abstract int Armor { get; protected set; }
        public abstract int Cargo { get; protected set; }

        public Vehicle(int fuel, int speed, int armor, int cargo)
        {
            Fuel = fuel;
            Speed = speed;
            Armor = armor;
            Cargo = cargo;
        }

        private void Movement(Vehicle vehicle)
        {
            int efficiency = vehicle.Speed / vehicle.Fuel;
        }

        private void Defence(Vehicle vehicle, int attack)
        {
            int defence = attack - vehicle.Armor;
        }

        
    }
}

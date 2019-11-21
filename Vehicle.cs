using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuturePunk
{
    class Vehicle
    {
        private readonly int fuel;
        private readonly int speed;
        private readonly int armor;

        public Vehicle(int fuel, int speed, int armor, string name)
        {
            this.fuel = fuel;
            this.speed = speed;
            this.armor = armor;
        }

        public static Vehicle A1 = new Vehicle(3, 1, 0, "A1");
        public static Vehicle A2 = new Vehicle(4, 2, 1, "A2");

        private void Movement(Vehicle vehicle)
        {
            int efficiency = vehicle.speed / vehicle.fuel;
        }

        private void Defence(Vehicle vehicle, int attack)
        {
            int defence = attack - vehicle.armor;
        }

        
    }
}

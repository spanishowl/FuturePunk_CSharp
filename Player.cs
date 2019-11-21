using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuturePunk
{
    class Player
    {
        private Vehicle vehicle;
        private string name;

        public Player(Vehicle vehicle, String name)
        {
            this.vehicle = vehicle;
            this.name = name;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public string getName()
        {
            return name;
        }

        public Vehicle GetVehicle()
        {
            return vehicle;
        }

    }
}

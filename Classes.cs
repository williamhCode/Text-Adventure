using System;
using System.Collections.Generic;

namespace Text_Adventure
{
    class Room
    {
        public string name;
        private string description;

        public Room north;
        public Room south;
        public Room west;
        public Room east;

        private List<Object> objects;

        public Room(string name, string description, List<Object> objects)
        {
            this.name = name;
            this.description = description;
            this.objects = objects;
        }
        
        public void SetConnections(Room north, Room west, Room south, Room east)
        {
            this.north = north;
            this.west = west;
            this.south = south;
            this.east = east;
        }

        override
        public string ToString()
        {
            return $"{name}\n{description}";
        }
    }

    class Object
    {
        private string name;
        private List<string> descriptions;

        public Object(string name, List<string> descriptions)
        {
            this.name = name;
            this.descriptions = descriptions;
        }
    }
}

using System;
using System.Collections.Generic;

namespace Text_Adventure
{
    class Room
    {
        public string name;
        private string description;

        private Room north;
        private Room south;
        private Room west;
        private Room east;

        private List<Object> objects;

        public Room(){}

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

    class Office:Room
    {
        
    }

    class Object
    {
        private string name;
        private string description;

        public Object(string name, string description)
        {
            this.name = name;
            this.description = description;
        }
    }
}

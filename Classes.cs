using System;
using System.Collections;

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

        private Object[] objects;

        public Room(){}

        public Room(string name, string description, Object[] objects)
        {
            this.name = name;
            this.description = description;
            this.objects = objects;
        }
        
        public void setConnections(Room north, Room south, Room west, Room east)
        {
            this.north = north;
            this.south = south;
            this.west = west;
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

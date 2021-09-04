using System;
using System.Collections;

namespace Text_Adventure
{
    class Room
    {
        private string name;
        private string description;

        private Room north;
        private Room south;
        private Room west;
        private Room east;

        private string[] objects;

        public Room(string name, string description, string[] objects)
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

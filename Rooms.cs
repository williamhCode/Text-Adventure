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

        public string getObjectName(string name)
        {
            Object obj = objects.Find(x => x.name.ToLower().Contains(name.ToLower()));
            if (obj == null)
                return "";
            return obj.name;
        }

        public Object GetObject(string name)
        {
            return objects.Find(x => x.name.ToLower().Equals(name.ToLower()));
        }

        public Object RemoveObject(string name)
        {
            Object obj = GetObject(name);
            objects.Remove(obj);
            return obj;
        }

        override
        public string ToString()
        {
            return $"{name}\n{description}";
        }
    }
}

using System;
using System.Collections.Generic;

namespace Text_Adventure
{
    class Player
    {
        private List<Object> objects;

        public Player(List<Object> objects)
        {
            this.objects = objects;
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

        public void AddObject(Object obj)
        {
            objects.Add(obj);
        }

        override
        public string ToString()
        {
            string output = "Items: ";
            foreach (Object obj in objects)
            {
                output += obj.name + ", ";
            }
            return output.Substring(0, output.Length - 2);
        }
    }
}

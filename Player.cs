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

        bool HasObjectName(Object obj, string name)
        {
            if (obj.name.ToLower().Equals(name))
            {
                return true;
            }
            string[] words = obj.name.ToLower().Split(' ');
            foreach (string word in words)
            {
                if (word.Equals(name))
                {
                    return true;
                }
            }
            return false;
        }

        public string GetObjectName(string name)
        {
            Object obj = objects.Find(obj => HasObjectName(obj, name));
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
            if (output.Equals("Items: "))
            {
                return "You have no items.";
            }
            return output.Substring(0, output.Length - 2);
        }
    }
}

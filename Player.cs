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

        public Object getObject(string name)
        {
            int index = objects.FindIndex(obj => obj.name.ToLower() == name);
            Object match = objects[index];
            objects.RemoveAt(index);
            return match;
        }

        public void addObject(Object obj)
        {
            objects.Add(obj);
        }
    }
}

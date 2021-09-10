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

        public void RemoveObject(string name)
        {
            objects.Remove(GetObject(name));
        }

        public void AddObject(Object obj)
        {
            objects.Add(obj);
        }
    }
}

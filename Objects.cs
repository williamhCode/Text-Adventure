using System;
using System.Collections.Generic;

namespace Text_Adventure
{
    delegate void Del(Object obj);

    class Object
    {
        public string name;
        private List<string> descriptions;

        private Del method;
        public bool locked = true;

        public Object(string name, List<string> descriptions)
        {
            this.name = name;
            this.descriptions = descriptions;
        }

        public void setInteractMethod(Del method)
        {
            this.method = method;
        }

        public void callInteractMethod()
        {
            this.method(this);
        }
    }

    class ObjectFunctions
    {
        public void Called(Object obj)
        {
            
        }
    }

}

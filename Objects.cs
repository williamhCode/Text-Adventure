using System;
using System.Collections.Generic;

namespace Text_Adventure
{
    delegate string Del(Object obj, string command, bool parameter = false);

    class Object
    {
        public string name;
        public List<string> descriptions;

        private Del method;

        public Object(string name, List<string> descriptions)
        {
            this.name = name;
            this.descriptions = descriptions;
        }

        public void SetInteractMethod(Del method)
        {
            this.method = method;
        }

        public string CallInteractMethod(string command, bool parameter = false)
        {
            return method(this, command, parameter);
        }
    }

    class ObjectFunctions
    {
        public string DJ(Object obj, string command, bool _)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            return "You cannot do that.";
        }

        public string VIPDoor(Object obj, string command, bool locked)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            else if (command.Equals("open"))
            {
                if (locked)
                {
                    Random rnd = new Random();
                    if (rnd.NextDouble() < 0.5)
                        return obj.descriptions[1];
                    else
                        return obj.descriptions[2];
                }
                else
                {
                    return obj.descriptions[3];
                }
            }
            return "You cannot do that";
        }

        public string MusicQueue(Object obj, string command, bool _)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            else if(command.Equals("use"))
            {
                return obj.descriptions[1];
            }
            else if(command.Equals("incorrect"))
            {
                return obj.descriptions[2];
            }
            else if(command.Equals("correct"))
            {
                return obj.descriptions[3];
            }
            else if(command.Equals("unlocked"))
            {
                return obj.descriptions[4];
            }

            return "You cannot do that";
        }
    }
}
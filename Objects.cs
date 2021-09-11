using System;
using System.Collections.Generic;

namespace Text_Adventure
{
    delegate string Del(Object obj, string command, int parameter = 0);

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

        public string CallInteractMethod(string command, int parameter = 0)
        {
            return method(this, command, parameter);
        }
    }

    class ObjectFunctions
    {
        public string DJ(Object obj, string command, int _)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            return "You cannot do that.";
        }

        public string VIPDoor(Object obj, string command, int locked)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            else if (command.Equals("open"))
            {
                if (locked == 1)
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
            return "You cannot do that.";
        }

        public string Gamblers(Object obj, string command, int stage)
        {
            if (command.Equals("examine"))
            {
                if (stage == 0)
                {
                    return obj.descriptions[0];
                }
                else if (stage == 1)
                {
                    return obj.descriptions[1];
                }
                else if (stage == 2)
                {
                    return obj.descriptions[2];
                }
                
            }
            return "You cannot do that.";
        }

        public string MusicQueue(Object obj, string command, int _)
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

            return "You cannot do that.";
        }
    }
}
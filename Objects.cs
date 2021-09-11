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

        public string VIPDoor(Object obj, string command, bool locked = false)
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
        public string Desk(Object obj, string comand)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0]
            }
            return "you cannot do that";
        }
        public string Computer(Object obj, string comand)
        {
            if (comand.Equals("examine")
            {
                return obj.descriptions[0]
            }
            else if (comand.Equals("use"))
            {
                return obj.descriptions[1]
            }
            return "you cannot do that";


        }
        public string LockedDoor(Object obj, string comand, bool locked = true)
        {
            if (comand.Equals("examine"))
            {
                return obj.descriptions[0]
            }
            else if (comand.Equals("use") && locked == true)
            {
                return obj descriptions[1];

            }
            else if (comand.Equals("use") && lockes == false)
            {
                return obj.descriptions[2]
            }
            return "you cannot do that";

        }
        public string boss(Object obj, string comand,bool havePen=false)
        {
            if (comand.Equals("examine") && havePen == false)
            {
                return obj.descriptions[0]
            }
            if (comand.Equals("examine") && havePen == true)
            {
                return obj.descriptions[1]
            }
            return "you cannot do that";

        }
        public string Mirror(object obj, string comand)
        {
            if (comand.Equals("examine"))
            {
                return obj.descriptions[0]
            }
            return "you cannot do that";

        }
        public string StallOne(object obj,string comand)
        {
            if (comand.Equals("examine"))
            {
                return obj.descriptions[0]
            }
            return "you cannot do that";


        }
        public string StallTwo(object obj, string comand)
        {
            if (comand.Equals("examine"))
            {
                return obj.descriptions[0]

            }
            return "you cannot do that";

        }
        public string StallThree(object obj, string comand)
        {
            if (comand.Equals("examine"))
            {
                return obj.descriptions[0]
            }
            return "you cannot do that";

        }
        public string StallFour(object obj, string comand)
        {
            if (comand.Equals("examine"))
            {
                return obj.descriptions[0]
            }
            return "you cannot do that";

        }
    }
}
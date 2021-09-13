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
        public string Desk(Object obj, string command, bool _)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            return "you cannot do that";
        }
        public string Computer(Object obj, string command, bool _)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            else if (command.Equals("use"))
            {
                return obj.descriptions[1];
            }
            return "you cannot do that";


        }
        public string LockedDoor(Object obj, string command, bool locked = true)
        {
            if (key == true)
            {
                locked = false;
            }
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            else if (command.Equals("use") && locked == true)
            {
                return obj.descriptions[1];

            }
            else if (command.Equals("use") && locked == false)
            {
                key = false;
                return obj.descriptions[2];
            }
            return "you cannot do that";

        }
        public string boss(Object obj, string command)
        {
            if (command.Equals("examine") && pen == false)
            {
                return obj.descriptions[0];
            }
            if (command.Equals("examine") && pen == true)
            {
                return obj.descriptions[1];
            }
            return "you cannot do that";

        }
        public string Mirror(Object obj, string command)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            return "you cannot do that";

        }
        public string StallOne(Object obj, string command)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            return "you cannot do that";


        }
        public string StallTwo(Object obj, string command)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];

            }
            return "you cannot do that";

        }
        public string StallThree(Object obj, string command)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            return "you cannot do that";

        }
        public string StallFour(Object obj, string command)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            return "you cannot do that";

        }
        public string Wall(Object obj, string command)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            if (command.Equals("use"))
            {
                return obj.descriptions[2];
            }
            return "you cannot do that";
        }
        public string smallGroup(Object obj, string command)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            return "you cannot do that";
        }
        public string yellowShirtCollegue(Object obj, string command)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            return "you cannot do that";
        }
        public string box(Object obj, string command)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            return "you cannot do that";
        }
        public string pen(Object obj, string command, bool change = false)
        {
            if (command.Equals("examine") && change == false)
            {
                return obj.descriptions[0];
            }
            if (command.Equals("examine") && change == true)
            {
                return obj.descriptions[2];
            }
            if (command.Equals("use"))
            {
                pen = true;
                change = true;
                return obj.descriptions[1];
            }
            return "you can't do that";

        }
        public string exitButton(Object obj, string command)
        {
            if (command.Equals("use"))
            {
                return obj.descriptions[0];
            }
            return "you can't do that";
        }
        public string shadyGuy(Object obj, string command)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            if (command.Equals("use") && coin = true)
            {
                return obj.descriptions[1];
            }
            return "you can't do that";

        }
        public string table(Object obj,string command)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            return "you can't do that";
        }
        public string couch(Object obj, string command)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            if (command.Equals("use"))
            {
                key = true;
                return obj.descriptions[1];
            }
            return "you can't do that";
        }
        public string note(Object obj,string command)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            return "you can't do that";
        }
        public string elavator(Object obj, string command)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            if (command.Equals("use") && key == false)
            {
                return obj.descriptions[1];

            }
            if (command.Equals("use") && key = true)
            {
                return obj.descriptions[2];
            }
            return "you can't do that";
        }



    }
}
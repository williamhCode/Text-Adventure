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
        bool pen = false;
        bool coin = false;
        bool ticket = false;
        bool key = false;
        bool music = false;
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
            if (ticket == true)
            {
                locked = true;
            }
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
                return obj.descriptions[0];
            }
            return "you cannot do that";
        }
        public string Computer(Object obj, string comand)
        {
            if (comand.Equals("examine")
            {
                return obj.descriptions[0];
            }
            else if (comand.Equals("use"))
            {
                return obj.descriptions[1];
            }
            return "you cannot do that";


        }
        public string LockedDoor(Object obj, string comand, bool locked = true)
        {
            if (key == true)
            {
                locked = false;
            }
            if (comand.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            else if (comand.Equals("use") && locked == true)
            {
                return obj descriptions[1];

            }
            else if (comand.Equals("use") && locked == false)
            {
                key = false;
                return obj.descriptions[2];
            }
            return "you cannot do that";

        }
        public string boss(Object obj, string comand)
        {
            if (comand.Equals("examine") && pen == false)
            {
                return obj.descriptions[0];
            }
            if (comand.Equals("examine") && pen == true)
            {
                return obj.descriptions[1];
            }
            return "you cannot do that";

        }
        public string Mirror(object obj, string comand)
        {
            if (comand.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            return "you cannot do that";

        }
        public string StallOne(object obj, string comand)
        {
            if (comand.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            return "you cannot do that";


        }
        public string StallTwo(object obj, string comand)
        {
            if (comand.Equals("examine"))
            {
                return obj.descriptions[0];

            }
            return "you cannot do that";

        }
        public string StallThree(object obj, string comand)
        {
            if (comand.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            return "you cannot do that";

        }
        public string StallFour(object obj, string comand)
        {
            if (comand.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            return "you cannot do that";

        }
        public string Wall(object obj, string comand)
        {
            if (comand.Equals("examine"))
            {
                return obj.description[0];
            }
            if (comand.Equals("use"))
            {
                return obj.description[2];
            }
            return "you cannot do that"
        }
        public string smallGroup(object obj, string comand)
        {
            if (comand.Equals("examine"))
            {
                return obj.description[0];
            }
            return "you cannot do that";
        }
        public string yellowShirtCollegue(object obj, string comand)
        {
            if (comand.Equals("examine"))
            {
                return obj.description[0];
            }
            if (comand.Equals("examine"))
            {
                return obj.description[0];
            }
            return "you cannot do that";
        }
        public string box(object obj, string comand)
        {
            if (comand.Equals("examine"))
            {
                return obj.description[0];
            }
            if (comand.Equals("examine"))
            {
                return obj.description[0];
            }
            return "you cannot do that";
        }
        public string pen(object obj, string comand, bool change = false)
        {
            if (comand.Equals("examine") && change == false)
            {
                return obj.description[0];
            }
            if (comand.Equals("examine") && change == true)
            {
                return obj.description[2];
            }
            if (comand.Equals("use"))
            {
                pen = true;
                change = true;
                return obj.description[1];
            }
            return "you can't do that";

        }
        public string exitButton(object obj, string comand)
        {
            if comand.Equals("use")
            {
                return obj.description[0];
            }
            return "you can't do that";
        }
        public string gamblers(object obj, string comand)
        {
            if (comand.Equals("examine"))
            {
                return obj.description[0];
            }
            if (comand.Equals("use") && music == false)
            {
                return obj.description[1];
            }
            if (comand.Equals("use") && music = true)
            {
                return obj.description[2];
                coin = true;
            }
            return "you can't do that";
        }
        public string goldenCoin(object obj, string comand)
        {
            if (comand.Equals("examine"))
            {
                return obj.description[0];
            }
            return "you can't do that";
        }
        public string shadyGuy(object obj, string comand)
        {
            if (comand.Equals("examine"))
            {
                return obj.description[0];
            }
            if (comand.Equals("use") && coin = true)
            {
                return obj.description[1];
            }
            return "you can't do that";

        }
        public string table(object obj,string comand)
        {
            if (comand.Equals("examine"))
            {
                return obj.description[0];
            }
            return "you can't do that";
        }
        public string couch(object obj, string comand)
        {
            if (comand.Equals("examine"))
            {
                return obj.description[0];
            }
            if (comand.Equals("use"))
            {
                key = true;
                return obj.description[1];
            }
            return "you can't do that";
        }
        public string note(object obj,string comand)
        {
            if (comand.Equals("examine"))
            {
                return obj.description[0];
            }
            return "you can't do that"
        }
        public string elavator(object obj, string comand)
        {
            if (comand.Equals("examine"))
            {
                return obj.description[0];
            }
            if (comand.Equals("use") && key == false)
            {
                return obj.descriptions[1];

            }
            if (comand.Equals("use") && key = true)
            {
                return obj.descriptions[2];
            }
            return "you can't do that";
        }



    }
}
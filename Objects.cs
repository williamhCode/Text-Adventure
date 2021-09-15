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
            if (command.Equals("examine") || command.Equals("talk to"))
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
            if (command.Equals("examine") || command.Equals("talk to"))
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

        public string USBDrive(Object obj, string command, int inserting)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            if (command.Equals("use"))
            {
                if (inserting == 1)
                {
                    return obj.descriptions[1];
                }
                return "You might want to use it on something.";
            }
            return "You cannot do that.";
        }

        public string GoldenCoin(Object obj, string command, int _)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            return "You cannot do that.";
        }

        public string MusicQueue(Object obj, string command, int unlocked)
        {
            if (unlocked == 0)
            {
                if (command.Equals("examine"))
                {
                    return obj.descriptions[0];
                }
                else if (command.Equals("use"))
                {
                    return obj.descriptions[1];
                }
                else if (command.Equals("incorrect"))
                {
                    return obj.descriptions[2];
                }
                else if (command.Equals("correct"))
                {
                    return obj.descriptions[3];
                }
                else if (command.Equals("unlocked"))
                {
                    return obj.descriptions[4];
                }
            }
            else
            {
                if (command.Equals("examine"))
                {
                    return obj.descriptions[5];
                }
                if (command.Equals("use"))
                {
                    return obj.descriptions[6];
                }
            }
            return "You cannot do that.";
        }

        public string Desk(Object obj, string command, int _)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            return "you cannot do that";
        }

        public string Computer(Object obj, string command, int _)
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

        public string LockedDoor(Object obj, string command, int _)
        {

            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            else if (command.Equals("openL"))
            {
                return obj.descriptions[1];
            }
            else if (command.Equals("openU"))
            {
                return obj.descriptions[2];
            }
            return "you cannot do that";

        }

        public string boss(Object obj, string command, int _)
        {
            if (command.Equals("examine") || command.Equals("talk to"))
            {
                return obj.descriptions[0];
            }
            if (command.Equals("give"))
            {
                return obj.descriptions[1];
            }



            return "you cannot do that";

        }

        public string Mirror(Object obj, string command, int _)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            return "you cannot do that";

        }

        public string StallOne(Object obj, string command, int _)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            return "you cannot do that";


        }

        public string StallTwo(Object obj, string command, int _)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];

            }
            return "you cannot do that";

        }

        public string StallThree(Object obj, string command, int _)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            return "you cannot do that";

        }

        public string StallFour(Object obj, string command, int _)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            return "you cannot do that";

        }

        public string wall(Object obj, string command, int _)
        {
            if (command.Equals("press"))
            {

                return obj.descriptions[0];
            }
            if (command.Equals("correct"))
            {
                return obj.descriptions[1];

            }
            if (command.Equals("wrong"))
            {
                return obj.descriptions[2];
            }
            return "you cannot do that";
        }

        public string smallGroup(Object obj, string command, int _)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            return "you cannot do that";
        }
        public string key(Object obj, string command, int _)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            return "you can't do that";
        }

        public string YellowShirtCollegue(Object obj, string command, int _)
        {
            if (command.Equals("examine") || command.Equals("talk to"))
            {
                return obj.descriptions[0];
            }
            return "you cannot do that";
        }

        public string Box(Object obj, string command, int _)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            if (command.Equals("move"))
            {
                return obj.descriptions[1];

            }
            return "you cannot do that";
        }

        public string Pen(Object obj, string command, int change = 0)
        {
            if (command.Equals("examine") && change == 0)
            {
                return obj.descriptions[0];
                change=1;
            }
           
            if (command.Equals("examine") && change == 2)
            {
                return obj.descriptions[2];
            }
            if (command.Equals("take"))
            {
                change = 2;
                return obj.descriptions[4];
                
            }
            if (command.Equals("give"))
            {
                
                return obj.descriptions[3];
            }
            if (command.Equals("taker"))
            {
                change = 2;
                return obj.descriptions[1];
            }
            return "you can't do that";

        }

        public string exitButton(Object obj, string command, int _)
        {
            if (command.Equals("press"))
            {
                return obj.descriptions[0];
            }
            return "you can't do that";
        }

        public string shadyGuy(Object obj, string command, int _)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            if (command.Equals("use"))
            {
                return obj.descriptions[1];
            }
            return "you can't do that";

        }

        public string table(Object obj, string command, int _)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            return "you can't do that";
        }

        public string couch(Object obj, string command, int _)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            if (command.Equals("use"))
            {

                return obj.descriptions[1];
            }
            return "you can't do that";
        }
        public string note(Object obj, string command, int _)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            return "you can't do that";
        }
        public string elavator(Object obj, string command, int _)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            if (command.Equals("use"))
            {
                return obj.descriptions[1];

            }
            if (command.Equals("use"))
            {
                return obj.descriptions[2];
            }
            return "you can't do that";
        }
    }
}
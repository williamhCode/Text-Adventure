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

        public string VIPDoor(Object obj, string command, int _)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            else if (command.Equals("open"))
            {
                return obj.descriptions[1];
            }
            return "You cannot do that.";
        }

        public string LetterHatch(Object obj, string command, int _)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
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
                else if (stage == 3)
                {
                    return obj.descriptions[3];
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
            else if (command.Equals("use") || command.Equals("put"))
            {
                if (inserting == 1)
                {
                    return obj.descriptions[1];
                }
                return "You might want to use it on something.";
            }
            return "You cannot do that.";
        }

        public string GoldenCoin(Object obj, string command, int givingToGuy)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            else if (command.Equals("give"))
            {
                if (givingToGuy == 1)
                {
                    return obj.descriptions[1];
                }
                return "Who to give to?";
            }

            return "You cannot do that.";
        }

        public string ShadyGuy(Object obj, string command, int _)
        {
            if (command.Equals("examine") || command.Equals("talk to"))
            {
                return obj.descriptions[0];
            }
            return "You cannot do that";
        }

        public string GoldenTicket(Object obj, string command, int unlockingDoor)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            else if(command.Equals("use") || command.Equals("put"))
            {
                if (unlockingDoor == 1)
                {
                    return obj.descriptions[1];
                }
                return "Use it on what?";
            }
            return "You cannot do that";
        }

        public string MusicQueue(Object obj, string command, int unlocked)
        {
            if (unlocked == 0)
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

        public string Elevator(Object obj, string command, int hasKey)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            return "You cannot do that.";
        }

        public string Table(Object obj, string command, int _)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            return "You cannot do that.";
        }
        
        public string Note(Object obj,string command, int _)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            return "You cannot do that.";
        }

        public string Couch(Object obj, string command, int _)
        {
            if (command.Equals("examine"))
            {
                return obj.descriptions[0];
            }
            return "You cannot do that.";
        }

        public string Key(Object obj, string command, int takingKey)
        {
            if (command.Equals("take") && takingKey == 1)
            {
                return obj.descriptions[0];
            }
            if (command.Equals("examine"))
            {
                return obj.descriptions[1];
            }
            return "You cannot do that.";
        }

        // public string Desk(Object obj, string command, bool _)
        // {
        //     if (command.Equals("examine"))
        //     {
        //         return obj.descriptions[0];
        //     }
        //     return "you cannot do that";
        // }

        // public string Computer(Object obj, string command, bool _)
        // {
        //     if (command.Equals("examine"))
        //     {
        //         return obj.descriptions[0];
        //     }
        //     else if (command.Equals("use"))
        //     {
        //         return obj.descriptions[1];
        //     }
        //     return "you cannot do that";
        // }

        // public string LockedDoor(Object obj, string command, bool locked = true)
        // {
        //     if (key == true)
        //     {
        //         locked = false;
        //     }
        //     if (command.Equals("examine"))
        //     {
        //         return obj.descriptions[0];
        //     }
        //     else if (command.Equals("use") && locked == true)
        //     {
        //         return obj.descriptions[1];

        //     }
        //     else if (command.Equals("use") && locked == false)
        //     {
        //         key = false;
        //         return obj.descriptions[2];
        //     }
        //     return "you cannot do that";

        // }

        // public string Boss(Object obj, string command)
        // {
        //     if (command.Equals("examine") && pen == false)
        //     {
        //         return obj.descriptions[0];
        //     }
        //     if (command.Equals("examine") && pen == true)
        //     {
        //         return obj.descriptions[1];
        //     }
        //     return "you cannot do that";

        // }

        // public string Mirror(Object obj, string command)
        // {
        //     if (command.Equals("examine"))
        //     {
        //         return obj.descriptions[0];
        //     }
        //     return "you cannot do that";
        // }

        // public string StallOne(Object obj, string command)
        // {
        //     if (command.Equals("examine"))
        //     {
        //         return obj.descriptions[0];
        //     }
        //     return "you cannot do that";
        // }

        // public string StallTwo(Object obj, string command)
        // {
        //     if (command.Equals("examine"))
        //     {
        //         return obj.descriptions[0];

        //     }
        //     return "you cannot do that";
        // }

        // public string StallThree(Object obj, string command)
        // {
        //     if (command.Equals("examine"))
        //     {
        //         return obj.descriptions[0];
        //     }
        //     return "you cannot do that";
        // }

        // public string StallFour(Object obj, string command)
        // {
        //     if (command.Equals("examine"))
        //     {
        //         return obj.descriptions[0];
        //     }
        //     return "you cannot do that";
        // }

        // public string Wall(Object obj, string command)
        // {
        //     if (command.Equals("examine"))
        //     {
        //         return obj.descriptions[0];
        //     }
        //     if (command.Equals("use"))
        //     {
        //         return obj.descriptions[2];
        //     }
        //     return "you cannot do that";
        // }

        // public string smallGroup(Object obj, string command)
        // {
        //     if (command.Equals("examine"))
        //     {
        //         return obj.descriptions[0];
        //     }
        //     return "you cannot do that";
        // }

        // public string yellowShirtCollegue(Object obj, string command)
        // {
        //     if (command.Equals("examine"))
        //     {
        //         return obj.descriptions[0];
        //     }
        //     if (command.Equals("examine"))
        //     {
        //         return obj.descriptions[0];
        //     }
        //     return "you cannot do that";
        // }

        // public string box(Object obj, string command)
        // {
        //     if (command.Equals("examine"))
        //     {
        //         return obj.descriptions[0];
        //     }
        //     if (command.Equals("examine"))
        //     {
        //         return obj.descriptions[0];
        //     }
        //     return "you cannot do that";
        // }

        // public string pen(Object obj, string command, bool change = false)
        // {
        //     if (command.Equals("examine") && change == false)
        //     {
        //         return obj.descriptions[0];
        //     }
        //     if (command.Equals("examine") && change == true)
        //     {
        //         return obj.descriptions[2];
        //     }
        //     if (command.Equals("use"))
        //     {
        //         pen = true;
        //         change = true;
        //         return obj.descriptions[1];
        //     }
        //     return "you can't do that";

        // }

        // public string exitButton(Object obj, string command)
        // {
        //     if (command.Equals("use"))
        //     {
        //         return obj.descriptions[0];
        //     }
        //     return "you can't do that";
        // }

        

        
    }
}
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

#pragma warning disable 0168

namespace Text_Adventure
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Room> ReadFloorLevel(string[] file_lines)
            {
                List<Room> rooms = new List<Room>();

                bool reading_room = false;
                int description_length = 0;

                string temp_room_name = null;
                string temp_room_description = null;

                List<Object> temp_objects = new List<Object>();
                string temp_object_name = null;
                List<string> temp_object_descriptions = new List<string>();

                foreach (string line in file_lines)
                {
                    if (line.Equals("") || line.Equals("."))
                    {
                        rooms.Add(new Room(temp_room_name, temp_room_description, temp_objects));
                        temp_objects = new List<Object>();
                        if (line.Equals("."))
                            break;
                    }
                    else if (line.Substring(0, 1) == "-")
                    {
                        temp_room_name = line.Substring(1);
                        reading_room = true;
                    }
                    else if (reading_room)
                    {
                        temp_room_description = line;
                        reading_room = false;
                    }
                    else if (description_length > 0)
                    {
                        description_length -= 1;
                        temp_object_descriptions.Add(line);
                        if (description_length == 0)
                        {
                            temp_objects.Add(new Object(temp_object_name, temp_object_descriptions));
                            temp_object_descriptions = new List<string>();
                        }
                    }
                    else if (Int32.TryParse(line.Substring(0, 1), out description_length))
                    {
                        temp_object_name = line.Substring(1);
                    }
                }

                return rooms;
            }

            string title = File.ReadAllText("levels/title.txt");
            string[] lines_2 = File.ReadAllLines("levels/2.txt");
            string[] lines_3 = File.ReadAllLines("levels/3.txt");

            List<Room> rooms_2 = ReadFloorLevel(lines_2);
            rooms_2[0].SetConnections(rooms_2[3], rooms_2[2], null, rooms_2[1]);
            rooms_2[1].SetConnections(null, rooms_2[0], null, null);
            rooms_2[2].SetConnections(null, null, null, rooms_2[0]);
            rooms_2[3].SetConnections(null, null, rooms_2[0], null);

            List<Room> rooms_3 = ReadFloorLevel(lines_3);
            rooms_3[0].SetConnections(rooms_3[1], rooms_3[2], rooms_3[3], rooms_3[5]);
            rooms_3[1].SetConnections(null, null, rooms_3[0], null);
            rooms_3[2].SetConnections(null, null, null, rooms_3[0]);
            rooms_3[3].SetConnections(rooms_3[0], rooms_3[4], null, null);
            rooms_3[4].SetConnections(null, null, null, rooms_3[3]);
            rooms_3[5].SetConnections(null, rooms_3[0], null, null);

            ObjectFunctions OF = new ObjectFunctions();
            rooms_2[0].GetObject("DJ").SetInteractMethod(OF.DJ);
            rooms_2[0].GetObject("VIP Door").SetInteractMethod(OF.VIPDoor);
            rooms_2[1].GetObject("Gamblers").SetInteractMethod(OF.Gamblers);
            rooms_2[2].GetObject("Music Queue").SetInteractMethod(OF.MusicQueue);
            rooms_3[0].GetObject("Computer").SetInteractMethod(OF.Computer);
            rooms_3[0].GetObject("Desk").SetInteractMethod(OF.Desk);
            rooms_3[0].GetObject("Door").SetInteractMethod(OF.LockedDoor);
            rooms_3[1].GetObject("Boss").SetInteractMethod(OF.boss);
            rooms_3[2].GetObject("Mirror").SetInteractMethod(OF.Mirror);
            rooms_3[2].GetObject("Stall One").SetInteractMethod(OF.StallOne);
            rooms_3[2].GetObject("Stall Two").SetInteractMethod(OF.StallTwo);
            rooms_3[2].GetObject("Stall Three").SetInteractMethod(OF.StallThree);
            rooms_3[2].GetObject("Stall Four").SetInteractMethod(OF.StallFour);
            rooms_3[2].GetObject("wall").SetInteractMethod(OF.wall);
            rooms_3[3].GetObject("Small Group").SetInteractMethod(OF.smallGroup);
            rooms_3[3].GetObject("Yellow Shirt Colleague").SetInteractMethod(OF.YellowShirtCollegue);
            rooms_3[4].GetObject("Box").SetInteractMethod(OF.Box);
            rooms_3[4].GetObject("Pen").SetInteractMethod(OF.Pen);
            rooms_3[5].GetObject("Exit Button").SetInteractMethod(OF.exitButton);
            rooms_3[2].GetObject("key").SetInteractMethod(OF.key);

            // game logic variables
            Player inventory = new Player(new List<Object>());

            Room currentRoom = rooms_3[0];
            List<Room> visitedRooms = new List<Room>() { currentRoom };

            bool verbose = true;

            // 0 = not talked to, 1 = talked to, 2 = music changed
            int gamblersStage = 0;
            bool USBgiven = false;
            bool coinGiven = false;

            // 0 = up, 1 = down, 2 = left, 3 = right
            int[] musicQueueCode = { 0, 0, 1, 1, 2, 3, 2, 3 };
            int musicQueueIndex = 0;
            bool usingMusicQueue = false;
            bool musicQueueUnlocked = false;

            // game functions
            void EnterRoom(Room room)
            {
                bool roomVisited = CheckRoomVisited(room);
                if (verbose || roomVisited == false)
                    Console.WriteLine(room + "\n");
                else
                    Console.WriteLine(room.name + "\n");
            }

            bool CheckRoomVisited(Room room)
            {
                foreach (Room visitedRoom in visitedRooms)
                    if (room == visitedRoom)
                        return true;
                visitedRooms.Add(room);
                return false;
            }

            var commandDict = new Dictionary<string, string>
            {
                {"look", "look"},
                {"l", "look"},
                {"go", "go"},
                {"move", "go"},
                {"walk", "go"},
                {"examine", "examine"},
                {"x", "examine"},
                {"look at", "examine"},
                {"inspect", "examine"},
                {"see", "examine"},
                {"open", "open"},
                {"use", "use"},
                {"put", "use"},
                {"inv", "inventory"},
                {"inventory", "inventory"},
                {"talk to", "talk to"},
                {"push", "move"},
                {"lift","move"},
                {"take","take"},
                {"pick up", "take"},
                {"grab","take"},
                {"press","press" },
                {"touch","press" },
                {"give","give" },

            };

            var directionDict = new Dictionary<string, string>
            {
                {"north","north"},
                {"n", "north"},
                {"forward", "north"},
                {"forwards", "north"},
                {"up", "north"},
                {"west","west"},
                {"w", "west"},
                {"left", "west"},
                {"south","south"},
                {"s", "south"},
                {"backward", "south"},
                {"backwards", "south"},
                {"down", "south"},
                {"east","east"},
                {"e", "east"},
                {"right", "east"},
            };

            var prepositionDict = new Dictionary<string, string>
            {
                {"in", "in"},
                {"on", "in"},
                {"to","to" },
            };

            (string command, string objectName, string otherObjectName) ParseInput(string input)
            {
                string command = null, objectName = null, otherObjectName = null;
                string preposition = "";
                char[] seperators = new char[] { ' ', ',', '.' };
                string[] words = input.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
                int input_length = words.Length;

                // 0 = verb, 1 = noun, 2 = ??
                int stage = 0;
                int index = 0;

                while (index < input_length)
                {
                    if (stage == 0)
                    {
                        if (commandDict.TryGetValue(words[index], out command) == false)
                        {
                            if (index + 1 <= input_length - 1)
                            {
                                if (commandDict.TryGetValue(words[index] + " " + words[index + 1], out command))
                                {
                                    index++;
                                }
                                else
                                {
                                    command = words[index];
                                }
                            }
                            else
                            {
                                command = words[index];
                            }
                        }
                        else
                        {
                            if (index + 1 <= input_length - 1)
                            {
                                string prevCommand = command;
                                if (commandDict.TryGetValue(words[index] + " " + words[index + 1], out command) == false)
                                {
                                    command = prevCommand;
                                }
                                else
                                {
                                    index++;
                                }
                            }
                        }
                    }
                    else if (stage == 1)
                    {
                        if (command.Equals("go"))
                        {
                            directionDict.TryGetValue(words[index], out objectName);
                        }
                        else
                        {
                            objectName = currentRoom.GetObjectName(words[index]);
                            if (objectName == "")
                            {
                                objectName = inventory.GetObjectName(words[index]);
                            }
                            string tempObjectName = objectName;
                            if (objectName != "")
                            {
                                if (index + 1 <= input_length - 1)
                                {
                                    objectName = currentRoom.GetObjectName(words[index] + " " + words[index + 1]);
                                    if (objectName == "")
                                    {
                                        objectName = inventory.GetObjectName(words[index] + " " + words[index + 1]);
                                    }
                                    if (objectName != "")
                                    {
                                        index++;
                                    }
                                    else
                                    {
                                        objectName = tempObjectName;
                                    }
                                }
                            }
                        }
                    }
                    else if (stage == 2)
                    {
                        prepositionDict.TryGetValue(words[index], out preposition);
                    }
                    else if (stage == 3)
                    {
                        if (preposition != null)
                        {
                            otherObjectName = currentRoom.GetObjectName(words[index]);
                            if (otherObjectName == "")
                            {
                                otherObjectName = inventory.GetObjectName(words[index]);
                            }
                            if (otherObjectName != "")
                            {
                                if (index + 1 <= input_length - 1)
                                {
                                    otherObjectName = currentRoom.GetObjectName(words[index] + " " + words[index + 1]);
                                    if (otherObjectName == "")
                                    {
                                        otherObjectName = inventory.GetObjectName(words[index] + " " + words[index + 1]);
                                    }
                                    if (otherObjectName != "")
                                    {
                                        index++;
                                    }
                                }
                            }
                        }
                    }
                    index++;
                    stage++;
                }

                if (command != null)
                {
                    command = command.ToLower();
                }
                if (objectName != null)
                {
                    objectName = objectName.ToLower();
                }
                if (otherObjectName != null)
                {
                    otherObjectName = otherObjectName.ToLower();
                }
                else
                {
                    otherObjectName = "";
                }

                return (command, objectName, otherObjectName);
            }

            // start game + game loop
            Console.WriteLine();
            Console.WriteLine(title + "\n");
            Console.WriteLine(currentRoom + "\n");
            int t = 0;
            int dl = 0;
            while (true)
            {
                Console.Write(">");
                string input = Console.ReadLine().ToLower().Trim();
                (string command, string objectName, string otherObjectName) = ParseInput(input);

                Console.WriteLine();
                int parameter = 0;
                int boxx = t;
                int door = dl;
                string output;

                if (usingMusicQueue)
                {
                    int codeAnswer = musicQueueCode[musicQueueIndex];
                    int inputedAnswer = 0;
                    bool invalidAnswer = false;

                    switch (input)
                    {
                        case "up":
                            inputedAnswer = 0;
                            break;

                        case "down":
                            inputedAnswer = 1;
                            break;

                        case "left":
                            inputedAnswer = 2;
                            break;

                        case "right":
                            inputedAnswer = 3;
                            break;

                        default:
                            invalidAnswer = true;
                            break;
                    }

                    if (invalidAnswer)
                    {
                        Console.WriteLine("That is not a choice.\n");
                    }
                    else if (inputedAnswer == codeAnswer)
                    {
                        if (musicQueueIndex < 7)
                        {
                            output = currentRoom.GetObject("Music Queue").CallInteractMethod("correct");
                            Console.WriteLine(output);
                            output = currentRoom.GetObject("Music Queue").CallInteractMethod("use");
                            Console.WriteLine(output + "\n");
                        }
                        else
                        {
                            output = currentRoom.GetObject("Music Queue").CallInteractMethod("unlocked");
                            Console.WriteLine(output + "\n");
                            usingMusicQueue = false;
                            musicQueueUnlocked = true;
                        }
                        musicQueueIndex += 1;
                    }
                    else
                    {
                        output = currentRoom.GetObject("Music Queue").CallInteractMethod("incorrect");
                        Console.WriteLine(output + "\n");
                        musicQueueIndex = 0;
                        usingMusicQueue = false;
                    }
                }
                else
                {
                    switch (command)
                    {
                        case "help":
                            break;

                        case "look":
                        case "l":
                            Console.WriteLine(currentRoom + "\n");
                            break;

                        case "go":
                            switch (objectName)
                            {
                                case "north":
                                    if (currentRoom.north == null)
                                        Console.WriteLine("There is nothing in the north.\n");
                                    else
                                    {
                                        currentRoom = currentRoom.north;
                                        EnterRoom(currentRoom);
                                    }
                                    break;

                                case "west":
                                    if (currentRoom.west == null)
                                        Console.WriteLine("There is nothing in the west.\n");
                                    else
                                    {
                                        currentRoom = currentRoom.west;
                                        EnterRoom(currentRoom);
                                    }
                                    break;

                                case "south":
                                    if (currentRoom.south == null)
                                        Console.WriteLine("There is nothing in the south.\n");
                                    else
                                    {
                                        currentRoom = currentRoom.south;
                                        EnterRoom(currentRoom);
                                    }
                                    break;

                                case "east":
                                    if (currentRoom.east == null)
                                        Console.WriteLine("There is nothing in the east.\n");
                                    if (currentRoom.east == rooms_3[5] && door == 0)
                                    {
                                        Console.WriteLine("The door is locked.\n");
                                    }

                                    else
                                    {
                                        currentRoom = currentRoom.east;
                                        EnterRoom(currentRoom);
                                    }
                                    break;

                                default:
                                    Console.WriteLine("Go where?\n");
                                    break;
                            }
                            break;

                        case "inventory":
                            if (objectName == null)
                            {
                                Console.WriteLine(inventory + "\n");
                            }
                            else
                            {
                                Console.WriteLine("You cannot do that.");
                            }
                            break;

                        case "examine":
                        case "talk to":
                            if (objectName == null)
                            {
                                Console.WriteLine("What to examine?\n");
                            }
                            else
                            {
                                if (objectName.Equals("gamblers"))
                                {
                                    parameter = gamblersStage;
                                    if (gamblersStage == 0)
                                    {
                                        gamblersStage = 1;
                                    }
                                    else if (gamblersStage == 1)
                                    {
                                        if (USBgiven == false)
                                        {
                                            currentRoom.GetObject("USB Drive").SetInteractMethod(OF.USBDrive);
                                            inventory.AddObject(currentRoom.RemoveObject("USB drive"));
                                        }
                                        USBgiven = true;
                                    }
                                    else if (gamblersStage == 2)
                                    {
                                        if (coinGiven == false)
                                        {
                                            currentRoom.GetObject("Golden Coin").SetInteractMethod(OF.GoldenCoin);
                                            inventory.AddObject(currentRoom.RemoveObject("Golden Coin"));
                                        }
                                        coinGiven = true;
                                        // gamblersStage = 3;
                                    }
                                    else if (gamblersStage == 3)
                                    {

                                    }

                                }
                                else if (objectName.Equals("music queue"))
                                {
                                    parameter = musicQueueUnlocked ? 1 : 0;
                                }
                                try
                                {
                                    output = currentRoom.GetObject(objectName).CallInteractMethod(command, parameter);
                                    Console.WriteLine(output + "\n");
                                }
                                catch (NullReferenceException e)
                                {
                                    try
                                    {
                                        output = inventory.GetObject(objectName).CallInteractMethod(command, parameter);
                                        Console.WriteLine(output + "\n");
                                    }
                                    catch (NullReferenceException _e)
                                    {
                                        Console.WriteLine("There's no such thing.\n");
                                    }
                                }
                            }

                            break;
                        case "move":
                            if (objectName == null)
                            {
                                Console.WriteLine("What to move?\n");
                            }
                            else
                            {

                                if (objectName.Equals("box"))
                                {

                                    output = currentRoom.GetObject(objectName).CallInteractMethod(command);
                                    t = 1;
                                    Console.WriteLine(output + "\n");
                                }
                                else
                                {
                                    try
                                    {
                                        output = currentRoom.GetObject(objectName).CallInteractMethod(command);

                                        Console.WriteLine(output + "\n");



                                    }

                                    catch (NullReferenceException e)
                                    {
                                        Console.WriteLine("You cannot do that\n");
                                    }
                                }
                            }
                            break;
                        case "take":
                            if (objectName == null)
                            {
                                Console.WriteLine("what to take\n");
                            }
                            else
                            {

                                if (objectName.Equals("pen") && boxx == 1)
                                {

                                    output = currentRoom.GetObject(objectName).CallInteractMethod("taker");
                                    inventory.AddObject(currentRoom.RemoveObject(objectName));
                                    Console.WriteLine(output + objectName + "\n");
                                    boxx = 0;
                                }
                                else
                                {
                                    try
                                    {
                                        output = currentRoom.GetObject(objectName).CallInteractMethod(command);
                                        Console.WriteLine(output + objectName + boxx + "55\n");
                                    }
                                    catch (NullReferenceException e)
                                    {
                                        Console.WriteLine("You cannot do that.\n");
                                    }
                                }

                            }
                            break;
                        case "give":
                            if (objectName == null)
                            {
                                Console.WriteLine("what to give\n");
                            }
                            else
                            {

                                if (objectName.Equals("pen") && otherObjectName.Equals("boss"))
                                {
                                    output = currentRoom.GetObject("boss").CallInteractMethod(command);
                                    Console.WriteLine(output + "\n");

                                    inventory.RemoveObject("pen");

                                }
                                else if (currentRoom == rooms_2[1])
                                {
                                    output = currentRoom.GetObject(objectName).CallInteractMethod(command);
                                    inventory.RemoveObject("Coin");
                                }
                                else
                                {
                                    try
                                    {
                                        output = currentRoom.GetObject(objectName).CallInteractMethod(command, parameter);
                                        Console.WriteLine(output + "\n");
                                    }
                                    catch (NullReferenceException e)
                                    {
                                        Console.WriteLine("You cannot do that.\n");
                                    }
                                }

                            }
                            break;
                        case "press":
                            if (objectName == null)
                            {
                                Console.WriteLine("what to press\n");
                            }
                            else
                            {
                                if (objectName.Equals("wall"))
                                {
                                    output = currentRoom.GetObject(objectName).CallInteractMethod(command);
                                    Console.WriteLine(output + "\n");
                                    string answer = Console.ReadLine().ToLower();
                                    if (answer.Equals("time"))
                                    {
                                        output = currentRoom.GetObject(objectName).CallInteractMethod("correct");
                                        inventory.AddObject(currentRoom.RemoveObject("key"));
                                        dl = 1;
                                        Console.WriteLine(output + "\n");
                                    }
                                    else
                                    {
                                        output = currentRoom.GetObject(objectName).CallInteractMethod("wrong");
                                        Console.WriteLine(output + "\n");
                                    }
                                }
                                try
                                {
                                    //output = currentRoom.GetObject(objectName).CallInteractMethod(command);
                                    //Console.WriteLine(output + "\n");
                                }
                                catch (NullReferenceException e)
                                {
                                    Console.WriteLine("You cannot do that.\n");
                                }
                            }
                            break;
                        case "open":
                            if (objectName == null)
                            {
                                Console.WriteLine("What to open?\n");
                            }
                            else
                            {
                                if (objectName.Equals("locked door"))
                                {
                                    if (inventory.GetObject("key") == null)
                                    {
                                        output = currentRoom.GetObject(objectName).CallInteractMethod("openL");
                                        Console.WriteLine(output + "\n");
                                    }
                                    else
                                    {
                                        output = currentRoom.GetObject(objectName).CallInteractMethod("openU");
                                        Console.WriteLine(output + "\n");
                                    }

                                }
                                try
                                {
                                    output = currentRoom.GetObject(objectName).CallInteractMethod(command);
                                    Console.WriteLine(output + "\n");
                                }
                                catch (NullReferenceException e)
                                {
                                    Console.WriteLine("You cannot do that.\n");
                                }
                            }
                            break;

                        case "use":
                            if (objectName == null)
                            {
                                Console.WriteLine("What to use?\n");
                            }
                            else
                            {
                                if (objectName.Equals("music queue"))
                                {
                                    if (musicQueueUnlocked)
                                    {
                                        parameter = 1;
                                    }
                                    else
                                    {
                                        usingMusicQueue = true;
                                    }
                                }
                                if (objectName.Equals("usb drive") && otherObjectName.Equals("music queue") && musicQueueUnlocked)
                                {
                                    parameter = 1;
                                    gamblersStage = 2;
                                }
                                try
                                {
                                    output = currentRoom.GetObject(objectName).CallInteractMethod(command, parameter);
                                    Console.WriteLine(output + "\n");
                                }
                                catch (NullReferenceException e)
                                {
                                    try
                                    {
                                        output = inventory.GetObject(objectName).CallInteractMethod(command, parameter);
                                        Console.WriteLine(output + "\n");
                                    }
                                    catch (NullReferenceException _e)
                                    {
                                        Console.WriteLine("You cannot do that.\n");
                                    }
                                }
                                if (objectName.Equals("usb drive") && otherObjectName.Equals("music queue") && musicQueueUnlocked)
                                {
                                    inventory.RemoveObject("usb drive");
                                }
                            }
                            break;


                        case "verbose":
                            verbose = true;
                            output = "THE OFFICE is now in its \"verbose\" mode, which always gives long descriptions of locations (even if you've been there before).";
                            Console.WriteLine(output + "\n");
                            break;

                        case "brief":
                            verbose = false;
                            output = "THE OFFICE is now in its normal \"brief\" printing mode, which gives long descriptions of places never before visited and short descriptions otherwise.";
                            Console.WriteLine(output + "\n");
                            break;

                        default:
                            output = $"I do not recognize \"{command}\".";
                            Console.WriteLine(output + "\n");
                            break;

                    }
                }
            }
        }
    }
}

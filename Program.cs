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
            rooms_2[0].GetObject("Letter Hatch").SetInteractMethod(OF.LetterHatch);
            rooms_2[1].GetObject("Gamblers").SetInteractMethod(OF.Gamblers);
            rooms_2[1].GetObject("Shady Guy").SetInteractMethod(OF.ShadyGuy);
            rooms_2[2].GetObject("Music Queue").SetInteractMethod(OF.MusicQueue);
            rooms_2[3].GetObject("Elevator").SetInteractMethod(OF.Elevator);
            rooms_2[3].GetObject("Table").SetInteractMethod(OF.Table);
            rooms_2[3].GetObject("Note").SetInteractMethod(OF.Note);
            rooms_2[3].GetObject("Couch").SetInteractMethod(OF.Couch);
            rooms_2[3].GetObject("Key").SetInteractMethod(OF.Key);

            rooms_3[0].GetObject("Computer").SetInteractMethod(OF.Computer);
            rooms_3[0].GetObject("Desk").SetInteractMethod(OF.Desk);
            rooms_3[0].GetObject("Locked Door").SetInteractMethod(OF.LockedDoor);
            rooms_3[1].GetObject("Boss").SetInteractMethod(OF.Boss);
            rooms_3[2].GetObject("Mirror").SetInteractMethod(OF.Mirror);
            rooms_3[2].GetObject("Stall One").SetInteractMethod(OF.StallOne);
            rooms_3[2].GetObject("Stall Two").SetInteractMethod(OF.StallTwo);
            rooms_3[2].GetObject("Stall Three").SetInteractMethod(OF.StallThree);
            rooms_3[2].GetObject("Stall Four").SetInteractMethod(OF.StallFour);
            rooms_3[2].GetObject("wall").SetInteractMethod(OF.Wall);
            rooms_3[3].GetObject("Small Group").SetInteractMethod(OF.SmallGroup);
            rooms_3[3].GetObject("Yellow Shirt Colleague").SetInteractMethod(OF.YellowShirtCollegue);
            rooms_3[4].GetObject("Box").SetInteractMethod(OF.Box);
            rooms_3[5].GetObject("Exit Button").SetInteractMethod(OF.ExitButton);
            rooms_3[2].GetObject("Key").SetInteractMethod(OF._Key);

            // game logic variables
            Player inventory = new Player(new List<Object>());

            Room currentRoom = rooms_3[0];
            List<Room> visitedRooms = new List<Room>() { currentRoom };

            bool verbose = true;

            // 0 = not talked to, 1 = talked to, 2 = music changed
            int gamblersStage = 0;
            bool USBgiven = false;

            // 0 = up, 1 = down, 2 = left, 3 = right
            int[] musicQueueCode = { 0, 0, 1, 1, 2, 3, 2, 3 };
            int musicQueueIndex = 0;
            bool usingMusicQueue = false;
            bool musicQueueUnlocked = false;
            
            bool boxMoved = false;
            bool doorLocked = true;
            bool penTaken = false;

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
                {"put", "put"},
                {"activate", "activate"},
                {"insert", "put"},
                {"inv", "inventory"},
                {"inventory", "inventory"},
                {"talk to", "talk to"},
                {"give", "give"},
                {"push", "move"},
                {"lift", "move"},
                {"take", "take"},
                {"pick up", "take"},
                {"grab", "take"},
                {"get", "take"},
                {"press", "press"},
                {"touch", "press"},
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
                {"to", "to"},
                {"under", "under"},
                {"below", "under"},
                {"beneath", "under"},
                {"with", "with"},
            };

            (string command, string objectName, string preposition, string otherObjectName, int input_length) ParseInput(string input)
            {
                string command = null, objectName = null, preposition = null, otherObjectName = null;

                char[] seperators = new char[] { ' ', ',', '.' };
                string[] words = input.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
                int input_length = words.Length;

                bool prepositionChecked = false;

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
                        prepositionChecked = true;
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
                if (preposition == null)
                {
                    if (prepositionChecked)
                    {
                        preposition = "INVALID";
                    }
                    else
                    {
                        
                    preposition = "";
                    }
                }
                if (otherObjectName != null)
                {
                    otherObjectName = otherObjectName.ToLower();
                }
                else
                {
                    otherObjectName = "";
                }

                return (command, objectName, preposition, otherObjectName, input_length);
            }

            // start game + game loop
            Console.WriteLine();
            Console.WriteLine(title + "\n");
            Console.WriteLine(currentRoom + "\n");

            while (true)
            {
                Console.Write(">");
                string input = Console.ReadLine().ToLower().Trim();
                (string command, string objectName, string preposition, string otherObjectName, int input_length) = ParseInput(input);

                Console.WriteLine();
                int parameter = 0;
                string output;

                if (command != null && objectName != null && preposition.Equals("INVALID"))
                {
                    Console.WriteLine("I understood up to the point that you want {0} {1}.\n", command, objectName);
                }

                else if (usingMusicQueue)
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
                            if (input_length > 1)
                            {
                                Console.WriteLine("Do you mean \"help?\"\n");
                            }
                            else
                            {
                                output = 
                                "look/l: look around the room\n" +
                                "go (n/w/s/e): move between rooms\n" +
                                "inventory/inv: check your inventory\n" +
                                "examine/x: inspect objects/people\n" +
                                "lift/push: move objects\n" +
                                "press: press things e.g. a button\n" + 
                                "open (with): open things, sometimes opening with objects\n" +
                                "use: use things\n" +
                                "give (to): give things to people\n" +
                                "take: take objects from surrounding\n" +
                                "-there are alternatives to many commands so try things around\n" +
                                "verbose: gives long descriptions of rooms before you enter\n" +
                                "brief: gives long descriptions only the first time you enter a room\n";
                                Console.WriteLine(output);
                            }
                            break;

                        case "look":
                        case "l":
                            if (input_length > 1)
                            {
                                Console.WriteLine("Do you mean \"look?\"\n");
                            }
                            else
                            {
                                Console.WriteLine(currentRoom + "\n");
                            }
                            break;

                        case "go":
                            switch (objectName)
                            {
                                case "north":
                                    if (currentRoom.north == null)
                                        Console.WriteLine("There is nothing in the north.\n");
                                    else
                                    {
                                        if (currentRoom.north.name.Equals("VIP ROOM"))
                                        {
                                            Console.WriteLine("The room is blocked by a locked door.\n");
                                        }
                                        else
                                        {
                                            currentRoom = currentRoom.north;
                                            EnterRoom(currentRoom);
                                        }
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
                                        if (currentRoom.south.name.Equals("DANCE FLOOR"))
                                        {
                                            Console.WriteLine("The room is blocked by a locked door.\n");
                                        }
                                        else
                                        {
                                            currentRoom = currentRoom.south;
                                            EnterRoom(currentRoom);
                                        }
                                    }
                                    break;

                                case "east":
                                    if (currentRoom.east == null)
                                        Console.WriteLine("There is nothing in the east.\n");
                                    else
                                    {
                                        if (currentRoom.east == rooms_3[5] && doorLocked)
                                        {
                                            Console.WriteLine("The door is locked.\n");
                                        }
                                        else
                                        {
                                            currentRoom = currentRoom.east;
                                            EnterRoom(currentRoom);
                                        }
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
                                    }
                                    else if (gamblersStage == 2)
                                    {
                                        currentRoom.GetObject("Golden Coin").SetInteractMethod(OF.GoldenCoin);
                                        inventory.AddObject(currentRoom.RemoveObject("Golden Coin"));
                                        gamblersStage = 3;
                                    }
                                }
                                else if (objectName.Equals("music queue"))
                                {
                                    parameter = musicQueueUnlocked ? 1 : 0;
                                }
                                else if (objectName.Equals("pen") && penTaken)
                                {
                                    parameter = 1;
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
                                if (objectName.Equals("box") && boxMoved == false)
                                {
                                    boxMoved = true;
                                    currentRoom.GetObject("Pen").SetInteractMethod(OF.Pen);
                                    currentRoom.description = "The room is a cluttered mess of janitorial supplies as well as the most random collection of things ever. There's also a box that has been moved.";
                                }
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
                                    Console.Write("Answer: ");
                                    string answer = Console.ReadLine().ToLower();
                                    Console.WriteLine();
                                    if (answer.Equals("time"))
                                    {
                                        output = currentRoom.GetObject(objectName).CallInteractMethod("correct");
                                        inventory.AddObject(currentRoom.RemoveObject("key"));
                                        Console.WriteLine(output + "\n");
                                        currentRoom.RemoveObject("wall");
                                        break;
                                    }
                                    else
                                    {
                                        output = currentRoom.GetObject(objectName).CallInteractMethod("wrong");
                                        Console.WriteLine(output + "\n");
                                        break;
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
                                if (objectName.Equals("exit button"))
                                {
                                    currentRoom = rooms_2[0];
                                    EnterRoom(currentRoom);
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
                                bool doorUnlocked = false;
                                if (objectName.Equals("locked door") && preposition.Equals("with") && otherObjectName.Equals("key"))
                                {
                                    doorUnlocked = true;
                                    doorLocked = false;
                                    parameter = 1;
                                }
                                try
                                {
                                    output = currentRoom.GetObject(objectName).CallInteractMethod(command, parameter);
                                    Console.WriteLine(output + "\n");
                                }
                                catch (NullReferenceException e)
                                {
                                    Console.WriteLine("You cannot do that.\n");
                                }
                                if (doorUnlocked)
                                {
                                    inventory.RemoveObject("Key");
                                    currentRoom.RemoveObject("Locked Door");
                                }
                            }
                            break;

                        case "use":
                        case "put":
                        case "activate":
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
                                bool driveUsed = false;
                                if (objectName.Equals("usb drive") && preposition.Equals("in") && otherObjectName.Equals("music queue") && musicQueueUnlocked)
                                {
                                    parameter = 1;
                                    if (gamblersStage == 1)
                                    {
                                        gamblersStage = 2;
                                    }
                                    driveUsed = true;
                                    musicQueueUnlocked = false;
                                    musicQueueIndex = 0;
                                }
                                bool ticketUsed = false;
                                if (objectName.Equals("golden ticket") && preposition.Equals("in") && otherObjectName.Equals("letter hatch"))
                                {
                                    parameter = 1;
                                    ticketUsed = true;
                                }
                                bool ending = false;
                                if (objectName.Equals("elevator") && preposition.Equals("with") && otherObjectName.Equals("key"))
                                {
                                    parameter = 1;
                                    ending = true;
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
                                if (driveUsed)
                                {
                                    inventory.RemoveObject("usb drive");
                                }
                                if (ticketUsed)
                                {
                                    inventory.RemoveObject("golden ticket");
                                    currentRoom = currentRoom.north;
                                    EnterRoom(currentRoom);
                                }
                                if (ending)
                                {
                                    Console.WriteLine("\n-- THE END --\n\n");
                                    return;
                                }
                            }
                            break;

                        case "give":
                            if (objectName == null)
                            {
                                Console.WriteLine("What to use?\n");
                            }
                            else
                            {
                                bool coinUsed = false;
                                if (objectName.Equals("golden coin") && preposition.Equals("to") && otherObjectName.Equals("shady guy"))
                                {
                                    parameter = 1;
                                    coinUsed = true;
                                    currentRoom.GetObject("Golden Ticket").SetInteractMethod(OF.GoldenTicket);
                                    inventory.AddObject(currentRoom.RemoveObject("Golden Ticket"));
                                    currentRoom.description = "As you examine the right corner of the room you see a small group of gamblers sitting around a table playing some sort of card game.";
                                }
                                bool penGiven = false;
                                if (objectName.Equals("pen") && preposition.Equals("to") && otherObjectName.Equals("boss"))
                                {
                                    penGiven = true;
                                    parameter = 1;
                                }
                                try
                                {
                                    output = inventory.GetObject(objectName).CallInteractMethod(command, parameter);
                                    Console.WriteLine(output + "\n");
                                }
                                catch (NullReferenceException e)
                                {
                                    Console.WriteLine("You cannot do that.\n");
                                }
                                if (coinUsed)
                                {
                                    inventory.RemoveObject("golden coin");
                                    currentRoom.RemoveObject("shady guy");
                                }
                                if(penGiven)
                                {
                                    inventory.RemoveObject("pen");
                                }
                            }
                            break;

                        case "take":
                            if (objectName == null)
                            {
                                Console.WriteLine("What to take?\n");
                            }
                            else
                            {
                                bool keyTaken = false;
                                if (currentRoom.GetObject("Key") != null && objectName.Equals("key") && preposition.Equals("under") && otherObjectName.Equals("couch"))
                                {
                                    keyTaken = true;
                                    parameter = 1;
                                    currentRoom.GetObject("Key").SetInteractMethod(OF.Key);
                                }
                                bool pen = false;
                                if (objectName.Equals("pen") && boxMoved)
                                {
                                    pen = true;
                                    penTaken = true;
                                }
                                try
                                {
                                    output = currentRoom.GetObject(objectName).CallInteractMethod(command, parameter);
                                    Console.WriteLine(output + "\n");
                                }
                                catch (NullReferenceException e)
                                {
                                    Console.WriteLine("You cannot do that.\n");
                                }
                                if (keyTaken)
                                {
                                    inventory.AddObject(currentRoom.RemoveObject("key"));
                                }
                                if(pen)
                                {
                                    inventory.AddObject(currentRoom.RemoveObject(objectName));
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

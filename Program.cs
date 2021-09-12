using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

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

            ObjectFunctions OF = new ObjectFunctions();
            rooms_2[0].GetObject("DJ").SetInteractMethod(OF.DJ);
            rooms_2[0].GetObject("VIP Door").SetInteractMethod(OF.VIPDoor);

            rooms_2[1].GetObject("Gamblers").SetInteractMethod(OF.Gamblers);

            rooms_2[2].GetObject("Music Queue").SetInteractMethod(OF.MusicQueue);

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

            // game logic variables
            Player inventory = new Player(new List<Object>());

            Room currentRoom = rooms_2[0];
            List<Room> visitedRooms = new List<Room>() { currentRoom };

            bool verbose = true;

            int gamblersStage = 0;
            bool USBgiven = false;
            
            // 0 = up, 1 = down, 2 = left, 3 = right
            int[] musicQueueCode = {0, 0, 1, 1, 2, 3, 2, 3}; 
            int musicQueueIndex = 0;
            bool usingMusicQueue = false;
            bool USBUnlocked = false;

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

            // start game + game loop
            Console.WriteLine();
            Console.WriteLine(title + "\n");
            Console.WriteLine(currentRoom + "\n");

            while (true)
            {
                Console.Write(">");
                string input = Console.ReadLine().ToLower().Trim();
                string command = input.IndexOf(" ") > -1 ? input.Substring(0, input.IndexOf(" ")) : input;
                string rest = input.IndexOf(" ") > -1 ? input.Substring(input.IndexOf(" ") + 1) : "";

                Console.WriteLine();
                int parameter = 0;
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
                            USBUnlocked = true;
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

                        case "north":
                        case "n":
                            if (currentRoom.north == null)
                                Console.WriteLine("There is nothing in the north.\n");
                            else
                            {
                                currentRoom = currentRoom.north;
                                EnterRoom(currentRoom);
                            }
                            break;

                        case "west":
                        case "w":
                            if (currentRoom.west == null)
                                Console.WriteLine("There is nothing in the west.\n");
                            else
                            {
                                currentRoom = currentRoom.west;
                                EnterRoom(currentRoom);
                            }
                            break;

                        case "south":
                        case "s":
                            if (currentRoom.south == null)
                                Console.WriteLine("There is nothing in the south.\n");
                            else
                            {
                                currentRoom = currentRoom.south;
                                EnterRoom(currentRoom);
                            }
                            break;

                        case "east":
                        case "e":
                            if (currentRoom.east == null)
                                Console.WriteLine("There is nothing in the east.\n");
                            else
                            {
                                currentRoom = currentRoom.east;
                                EnterRoom(currentRoom);
                            }
                            break;

                        case "inventory":
                        case "inv":
                            if (rest.Equals(""))
                            {
                                Console.WriteLine(inventory + "\n");
                            }
                            else
                            {
                                try
                                {
                                    output = inventory.GetObject(rest).CallInteractMethod("examine");
                                    Console.WriteLine(output + "\n");
                                }
                                catch (NullReferenceException e)
                                {
                                    Console.WriteLine("There's no such thing.\n");
                                }    
                            }
                            
                            break;

                        case "examine":
                        case "x":
                            if (rest.Equals(""))
                            {
                                Console.WriteLine("What to examine?\n");
                            }
                            else
                            {   
                                if (rest.Equals("gamblers"))
                                {
                                    parameter = gamblersStage;
                                    if (USBgiven == false)
                                    {
                                        if (gamblersStage == 0)
                                        {
                                            gamblersStage = 1;
                                        }
                                        else if (gamblersStage == 1)
                                        {  
                                            USBgiven = true;
                                            inventory.AddObject(currentRoom.RemoveObject("USB drive"));
                                        }
                                    }
                                }
                                try
                                {
                                    output = currentRoom.GetObject(rest).CallInteractMethod("examine", parameter);
                                    Console.WriteLine(output + "\n");
                                }
                                catch (NullReferenceException e)
                                {
                                    Console.WriteLine("There's no such thing.\n");
                                }    
                            }
                            break;

                        case "open":
                            if (rest.Equals(""))
                            {
                                Console.WriteLine("What to open?\n");
                            }
                            else
                            {
                                try
                                {
                                    output = currentRoom.GetObject(rest).CallInteractMethod("open");
                                    Console.WriteLine(output + "\n");
                                }
                                catch (NullReferenceException e)
                                {
                                    Console.WriteLine("You cannot open that.\n");
                                }    
                            }
                            break;

                        case "use":
                            if (rest.Equals(""))
                            {
                                Console.WriteLine("What to use?\n");
                            }
                            else
                            {
                                if (rest.Equals("music queue"))
                                {
                                    usingMusicQueue = true;
                                }
                                try
                                {
                                    output = currentRoom.GetObject(rest).CallInteractMethod("use");
                                    Console.WriteLine(output + "\n");
                                }
                                catch (NullReferenceException e)
                                {
                                    Console.WriteLine("You cannot use that.\n");
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
                            output = $"\"{command}\" is not a command.";
                            Console.WriteLine(output + "\n");
                            break;

                    }
                }
            }
        }
    }
}

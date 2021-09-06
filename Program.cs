using System;
using System.IO;
using System.Collections.Generic;

namespace Text_Adventure
{
    class Program
    {
        static void Main(string[] args)
        {
            string title = File.ReadAllText("levels/title.txt");
            string[] lines_3 = File.ReadAllLines("levels/3.txt");

            bool reading_room = false;
            bool reading_object = false;

            List<Room> rooms_3 = new List<Room>();
            string temp_room_name = null;
            string temp_room_description = null;

            List<Object> temp_objects = new List<Object>();
            string temp_object_name = null;
            string temp_object_description = null;

            foreach (string line in lines_3)
            {
                if (line.Equals(""))
                {
                    rooms_3.Add(new Room(temp_room_name, temp_room_description, temp_objects));
                    temp_objects = new List<Object>();
                }
                else if (line.Substring(0, 1) == "-")
                {
                    temp_room_name = line.Substring(1);
                    reading_room = true;
                }
                else if (line.Substring(0, 1) == "*")
                {
                    temp_object_name = line.Substring(1);
                    reading_object = true;
                }
                else if (reading_room)
                {
                    temp_room_description = line;
                    reading_room = false;
                }
                else if (reading_object)
                {
                    temp_object_description = line;
                    temp_objects.Add(new Object(temp_object_name, temp_object_description));
                    reading_object = false;
                }
            }

            rooms_3[0].SetConnections(rooms_3[1], rooms_3[2], rooms_3[3], rooms_3[5]);
            rooms_3[1].SetConnections(null, null, rooms_3[0], null);
            rooms_3[2].SetConnections(null, null, null, rooms_3[0]);
            rooms_3[3].SetConnections(rooms_3[0], rooms_3[4], null, null);
            rooms_3[4].SetConnections(null, null, null, rooms_3[3]);
            rooms_3[5].SetConnections(null, rooms_3[0], null, null);

            // logic variables
            bool verbose = true;

            // start game
            Console.WriteLine();
            Console.WriteLine(title + "\n");

            Room currentRoom = rooms_3[0];
            Console.WriteLine(currentRoom + "\n");
            while (true)
            {
                Console.Write(">");
                string input = Console.ReadLine();
                Console.WriteLine();
                switch (input)
                {
                    case "look":
                    case "l":
                        // String output;
                        // if (verbose)
                        //     output = currentRoom;
                        // else
                        //     output = currentRoom.name;
                        Console.WriteLine(currentRoom + "\n");
                        break;

                    case "verbose":
                        verbose = true;
                        string output = "THE OFFICE is now in its \"verbose\" mode, which always gives long descriptions of locations (even if you've been there before).";
                        Console.WriteLine(output + "\n");
                        break;

                    case "brief":
                        verbose = false;
                        output = "THE OFFICE is now in its normal \"brief\" printing mode, which gives long descriptions of places never before visited and short descriptions otherwise.";
                        Console.WriteLine(output + "\n");
                        break;

                    default:
                        output = $"I do not recognize \"{(input.IndexOf(" ") > -1 ? input.Substring(0, input.IndexOf(" ")) : input)}\"";
                        Console.WriteLine(output + "\n");
                        break;

                }
            }
        }
    }
}

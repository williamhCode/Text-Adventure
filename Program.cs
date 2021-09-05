using System;

namespace Text_Adventure
{
    class Program
    {
        static void Main(string[] args)
        {
            bool verbose = true;

            string name = "OFFICE";
            string description = 
                "You are surrounded by a multitude of people, each busily walking around or situation in their desk. " +
                "The space is rather large and looks just like a regular office. " +
                "There is a door leading to another room in the north and a bathroom on the right.";
            Room office = new Room(name, description, null);

            Console.WriteLine();

            string title = 
                "THE OFFICE\n" +
                "You wake up abrubtly. Confused, you look at your surroundings and see that you are sitting in a chair with a desk in front of you. " +
                "You try to think of what happened previously, but you cannot remember anything. However, you only know that your name is Billy.";
            Console.WriteLine(title + "\n");

            Room currentRoom = office;
            Console.WriteLine(currentRoom + "\n");
            while(true)
            {
                Console.Write(">");
                string input = Console.ReadLine();
                Console.WriteLine();
                switch(input)
                {
                    case "look":
                    // String output;
                    // if (verbose)
                    //     output = currentRoom;
                    // else
                    //     output = currentRoom.name;
                    Console.WriteLine(currentRoom + "\n");
                    break;

                    case "verbose":
                    verbose = true;
                    break;

                    case "brief":
                    verbose = false;
                    break;

                }
            }
        }
    }
}

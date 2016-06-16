namespace BunnyWars.Core
{
    using System;
    using System.Linq;

    public class BunnyWarsTestingGround
    {
        static void Main(string[] args)
        {
            BunnyWarsStructure bunnywar = new BunnyWarsStructure();
            
            var input = Console.ReadLine().Split(' ').ToList();
            switch (input[0])
            {
                case "Add":
                    if (input.Count > 1)
                    {
                        bunnywar.AddBunny(input[1], int.Parse(input[2]), int.Parse(input[3]));
                    }
                    else
                    {
                        bunnywar.AddRoom(int.Parse(input[1]));
                    }
                    break;
                case "BunnyCount":
                    Console.WriteLine(bunnywar.BunnyCount);
                    break;
                case "RoomCount":
                    Console.WriteLine(bunnywar.RoomCount);
                    break;
                case "Remove":
                    var roomID = int.Parse(input[1]);
                    bunnywar.Remove(roomID);
                    break;
                case "Next":
                    bunnywar.Next(input[1]);
                    break;
                case "Previous":
                    bunnywar.Previous(input[1]);
                    break;
                case "Detonate":
                    bunnywar.Detonate(input[1]);
                    break;
                case "ListBunniesByTeam":
                    var team = int.Parse(input[1]);
                    bunnywar.ListBunniesByTeam(team);
                    break;
                case "ListBunniesBySuffix":
                    bunnywar.ListBunniesBySuffix(input[1]);
                    break;
            }
        }
    }
}

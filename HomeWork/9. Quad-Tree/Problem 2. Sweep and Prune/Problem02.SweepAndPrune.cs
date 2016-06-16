namespace Problem_2.Sweep_and_Prune
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SweepandPrune
    {
        public static int counter = 0;

        static void Main(string[] args)
        {
            var userInput = Console.ReadLine().Split(' ').ToList();
            bool startGame = false;
            var gameObjectList = new List<GameObject>();
            while (!startGame)
            {
                switch (userInput[0])
                {
                    case "add":
                        int xCoord = int.Parse(userInput[2]);
                        int yCoord = int.Parse(userInput[3]);
                        var gameObject = new GameObject(xCoord, yCoord, userInput[1]);
                        gameObjectList.Add(gameObject);
                        break;
                    case "start":
                        startGame = true;
                        break;
                }

                userInput = Console.ReadLine().Split(' ').ToList();
            }

            counter = 1;
            while (true)
            {
                switch (userInput[0])
                {
                    case "tick":
                        SweepAndPrune(gameObjectList);
                        break;
                    case "move":
                        var personIndex = FindObject(userInput[1], gameObjectList);
                        if (personIndex != -1)
                        {
                            int newX1 = int.Parse(userInput[2]);
                            int newY1 = int.Parse(userInput[3]);
                            MoveCharacter(newX1, newY1, gameObjectList, personIndex);
                            InsertionSort(gameObjectList);
                            SweepAndPrune(gameObjectList);
                        }
                        break;
                }

                counter++;
                userInput = Console.ReadLine().Split(' ').ToList();
            }
        }

        private static void MoveCharacter(int newX1, int newY1, List<GameObject> objectList, int index)
        {
            objectList[index].X1 = newX1;
            objectList[index].Y1 = newY1;
            objectList[index].X2 = objectList[index].X1 + objectList[index].Width;
            objectList[index].Y2 = objectList[index].Y1 + objectList[index].Height;
        }

        private static int FindObject(string name, List<GameObject> objects)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if (name == objects[i].Name)
                {
                    return i;
                }
            }

            return -1;
        }

        private static void SweepAndPrune(List<GameObject> objects)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                var currentObj = objects[i];
                for (int j = i + 1; j < objects.Count; j++)
                {
                    var candidateCollision = objects[j];
                    if (currentObj.X2 < candidateCollision.X1)
                    {
                        break;
                    }
                    else if (currentObj.Intersects(candidateCollision))
                    {
                        Console.WriteLine("({0}) {1} collides with {2}", counter, currentObj.Name, candidateCollision.Name);
                    }
                }
            }
        }

        private static void InsertionSort(List<GameObject> objects)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                int currentIndex = i;

                for (int j = i - 1; j >= 0; j--)
                {
                    if (objects[i].X1 < objects[j].X1)
                    {
                        var temp = objects[j];
                        objects[j] = objects[currentIndex];
                        objects[currentIndex] = temp;
                        currentIndex--;
                    }
                }
            }
        }
    }
}

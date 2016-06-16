namespace BunnyWars.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class BunnyComparer : IComparer<Bunny>
    {
        public int Compare(Bunny x, Bunny y)
        {
            var name1 = x.Name.Reverse().ToArray();
            var name2 = y.Name.Reverse().ToArray();
            int min = Math.Min(name1.Count(), name2.Count());

            for (int i = 0; i < min; i++)
            {
                if (name1[i] != name2[i])
                {
                    return name1[i].CompareTo(name2[i]);
                }
            }

            int xName = x.Name.Length;
            int yName = y.Name.Length;
            return xName.CompareTo(yName);
        }
    }

    public class BunnyWarsStructure : IBunnyWarsStructure
    {
        private const int TeamCount = 5;
        private static IComparer<Bunny> SuffixComparator = new BunnyComparer();

        OrderedSet<int> chainedRooms =
            new OrderedSet<int>();
        public OrderedDictionary<string, Bunny> GetBunnies =
            new OrderedDictionary<string, Bunny>();
        private OrderedSet<Bunny> bunniesOrderedBySuffix =
            new OrderedSet<Bunny>(SuffixComparator);
        Dictionary<int, Dictionary<int, SortedSet<Bunny>>> bunnyStructure =
            new Dictionary<int, Dictionary<int, SortedSet<Bunny>>>();

        public int BunnyCount => GetBunnies.Count;

        public int RoomCount => this.bunnyStructure.Keys.Count;

        private OrderedSet<Bunny>[] bunniesByTeam = new OrderedSet<Bunny>[TeamCount];

        public void AddRoom(int roomId)
        {

            if (bunnyStructure.ContainsKey(roomId))
            {
                throw new ArgumentException();
            }

            bunnyStructure[roomId] = new Dictionary<int, SortedSet<Bunny>>();
            this.chainedRooms.Add(roomId);
        }

        public void AddBunny(string name, int team, int roomId)
        {
            if (GetBunnies.ContainsKey(name))
            {
                throw new ArgumentException();
            }
            if (!bunnyStructure.ContainsKey(roomId))
            {
                throw new ArgumentException();
            }
            if (bunniesByTeam[team] == null)
            {
                bunniesByTeam[team] = new OrderedSet<Bunny>();
            }
            if (!bunnyStructure[roomId].ContainsKey(team))
            {
                bunnyStructure[roomId][team] = new SortedSet<Bunny>();
            }

            var newBunny = new Bunny(name, team, roomId);

            GetBunnies[name] = newBunny;
            bunniesOrderedBySuffix.Add(newBunny);
            bunniesByTeam[team].Add(newBunny);
            bunnyStructure[roomId][newBunny.Team].Add(newBunny);
        }

        public void Remove(int roomId)
        {
            if (!bunnyStructure.ContainsKey(roomId))
            {
                throw new ArgumentException();
            }

            var bunniesToRemove = bunnyStructure[roomId];
            foreach (var bunnies in bunniesToRemove)
            {
                foreach (var bunny in bunnies.Value)
                {
                    bunniesByTeam[bunny.Team].Remove(bunny);
                    bunniesOrderedBySuffix.Remove(bunny);
                    GetBunnies.Remove(bunny.Name);
                }
            }

            this.chainedRooms.Remove(roomId);
            bunnyStructure.Remove(roomId);
        }

        public void Next(string bunnyName)
        {
            if (!GetBunnies.ContainsKey(bunnyName))
            {
                throw new ArgumentException();
            }

            var getBunny = GetBunnies[bunnyName];
            int roomIndex = chainedRooms.IndexOf(getBunny.RoomId) + 1;
            MoveBunny(roomIndex, getBunny);
        }

        public void Previous(string bunnyName)
        {
            if (!GetBunnies.ContainsKey(bunnyName))
            {
                throw new ArgumentException();
            }
            
            var getBunny = GetBunnies[bunnyName];
            int roomIndex = chainedRooms.IndexOf(getBunny.RoomId) - 1;
            MoveBunny(roomIndex, getBunny);
        }

        private void MoveBunny(int roomIndex, Bunny getBunny)
        {
            if (roomIndex == this.chainedRooms.Count)
            {
                roomIndex = 0;
            }
            else if (roomIndex == -1)
            {
                roomIndex = this.chainedRooms.Count - 1;
            }

            var targetRoom = this.chainedRooms[roomIndex];
            bunnyStructure[getBunny.RoomId][getBunny.Team].Remove(getBunny);

            if (!bunnyStructure.ContainsKey(targetRoom))
            {
                throw new ArgumentException();
            }
            if (!bunnyStructure[targetRoom].ContainsKey(getBunny.Team))
            {
                bunnyStructure[targetRoom][getBunny.Team] = new SortedSet<Bunny>();
            }

            getBunny.RoomId = targetRoom;
            bunnyStructure[targetRoom][getBunny.Team].Add(getBunny);
        }

        public void Detonate(string bunnyName)
        {
            if (!GetBunnies.ContainsKey(bunnyName))
            {
                throw new ArgumentException();
            }

            var bunnyDetonar = GetBunnies[bunnyName];

            var affectedBunnies = bunnyStructure[bunnyDetonar.RoomId].Where(s => s.Key != bunnyDetonar.Team);
            if (!affectedBunnies.Any())
            {
                return;
            }
            SortedSet<Bunny> bunneisTOremove = new SortedSet<Bunny>();
            var testEnumerator = affectedBunnies.GetEnumerator();

            while (testEnumerator.MoveNext())
            {
                var bunnyEnumerator = testEnumerator.Current.Value.GetEnumerator();
                while (bunnyEnumerator.MoveNext())
                {
                    var bunny = bunnyEnumerator.Current;
                    bunny.Health -= 30;
                    if (bunny.Health <= 0)
                    {
                        bunneisTOremove.Add(bunny);
                        bunnyDetonar.Score++;
                    }
                }
            }

            if (bunneisTOremove.Count > 0)
            {
                foreach (var bunny in bunneisTOremove)
                {
                    bunniesByTeam[bunny.Team].Remove(bunny);
                    bunniesOrderedBySuffix.Remove(bunny);
                    bunnyStructure[bunny.RoomId][bunny.Team].Remove(bunny);
                    GetBunnies.Remove(bunny.Name);
                }
            }

            bunneisTOremove.Clear();
        }

        public IEnumerable<Bunny> ListBunniesByTeam(int team)
        {
            return bunniesByTeam[team];
        }

        public IEnumerable<Bunny> ListBunniesBySuffix(string suffix)
        {
            var low = new Bunny(suffix, 0, 0);
            var high = new Bunny(char.MaxValue + suffix, 0, 0);

            return this.bunniesOrderedBySuffix.Range(low, true, high, false);
        }
    }
}


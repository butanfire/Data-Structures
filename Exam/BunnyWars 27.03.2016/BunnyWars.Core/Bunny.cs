namespace BunnyWars.Core
{
    using System;

    public class Bunny : IComparable<Bunny>
    {
        private const int BunnyHealth = 100;
        private const int StartingScore = 0;

        public Bunny(string name, int team, int roomId)
        {
            this.Name = name;
            this.Team = team;
            this.RoomId = roomId;
            this.Health = BunnyHealth;
            this.Score = StartingScore;
        }

        public int RoomId { get; set; }

        public string Name { get; private set; }

        public int Health { get; set; }

        public int Score { get; set; }

        public int Team { get; private set; }

        public int CompareTo(Bunny other)
        {
            return other.Name.CompareTo(this.Name);
        }
    }
}

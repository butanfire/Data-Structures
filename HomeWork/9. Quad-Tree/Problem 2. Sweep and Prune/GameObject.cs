namespace Problem_2.Sweep_and_Prune
{
    public class GameObject
    {
        private const int width = 10;
        private const int height = 10;

        public GameObject(int x1, int y1, string name, int width = 10, int height = 10)
        {
            this.X1 = x1;
            this.Y1 = y1;
            this.X2 = x1 + width;
            this.Y2 = y1 + height;
            this.Name = name;
        }

        public int Y1 { get; set; }

        public int X1 { get; set; }

        public int Y2 { get; set; }

        public int X2 { get; set; }

        public int Height
        {
            get
            {
                return height;
            }
        }

        public int Width
        {
            get
            {
                return width;
            }
        }

        public string Name { get; set; }

        public bool Intersects(GameObject other)
        {
            return this.X1 <= other.X2 &&
                    other.X1 <= this.X2 &&
                    this.Y1 <= other.Y2 &&
                    other.Y1 <= this.Y2;
        }

        public override string ToString()
        {
            return string.Format("({0}, {1}) .. ({2}, {3})",
                this.X1, this.Y1, this.X2, this.Y2);
        }
    }
}


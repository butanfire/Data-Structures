﻿namespace Problem_2.TraverseDirectory
{
    public class File
    {
        public File(string name, long size)
        {
            this.Name = name;
            this.Size = size;
        }

        public string Name { get; private set; }

        public long Size { get; private set; }
    }
}

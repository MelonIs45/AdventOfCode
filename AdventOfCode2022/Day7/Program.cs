namespace Day7
{
    internal class Program
    {
        public static Directory Root = new ("/", false, 0, parent: null);
        public const int MaxSize = 70000000;
        public const int NeededSize = 30000000;
        public const int MaxDirectorySize = 100000;
        
        static void Main(string[] args)
        {
            (int, int) result = Solve();
            Console.WriteLine(result.Item1);
            Console.WriteLine(result.Item2);
        }
        
        public static (int, int) Solve()
        {
            string[] input = File.ReadAllLines("input.txt");
            
            foreach (string command in input)
            {
                string[] split = command.Split(" ");
                if (!split[1].Equals("ls"))
                {
                    FileItem fileItem = new FileItem(split);
                    Root = fileItem.Perform(Root);
                }
            }

            Root.Parent.CalculateSizeOfDirectories();
            
            int amountToRemove = NeededSize - (MaxSize - Root.Parent.Size);
            List<Directory> directories = Root.Parent.GetDirectories();
            Directory smallest = Root.Parent;

            for (int i = 0; i < directories.Count; i++)
                if (directories[i].Size < smallest.Size && directories[i].Size > amountToRemove)
                    smallest = directories[i];

            return (Root.Parent.CalculateSize(), smallest.Size);
        }
    }

    class FileItem
    {
        public string Property { get; set; }
        public string Params { get; set; }
        
        public FileItem(string[] @params)
        {
            if (@params.Length == 3)
            {
                Property = @params[1];
                Params = @params[2];
            }
            else
            {
                Property = @params[0];
                Params = @params[1];
            }
        }
        
        internal Directory Perform(Directory root)
        {
            Directory currentDirectory = root;

            switch (Property)
            {
                case "dir":
                    currentDirectory.AddFile(Params);
                    break;
                case "cd":
                    currentDirectory = currentDirectory.GetDirectory(Params);
                    break;
                default:
                    currentDirectory.AddFile(Params, Property);
                    break;
            }

            return currentDirectory;
        }
    }

    class Directory
    {
        public string Name { get; set; }
        public bool File { get; set; }
        public int Size { get; set; }
        public Directory Parent { get; set; }
        public List<Directory> Children { get; set; }
        public int Depth { get; set; }
        public bool Checked { get; set; }

        public Directory(string name, bool file, int size, Directory parent)
        {
            Name = name;
            File = file;
            Size = size;
            Parent = parent;
            Children = new List<Directory>();
            if (parent != null)
                Depth = parent.Depth + 1;
        }

        internal void AddFile(string fileName, string @params)
        {
            this.Children.Add(new Directory(fileName, true, int.Parse(@params), this));
        }

        internal void AddFile(string fileName)
        {
            this.Children.Add(new Directory(fileName, false, 0, this));
        }

        internal Directory GetDirectory(string @params)
        {
            switch (@params)
            {
                case "/":
                    return Program.Root;
                case "..":
                    return this.Parent;
                default:
                    if (!this.Children.Any(x => x.Name == @params))
                        this.AddFile(@params); 
                    return this.Children.Find(x => x.Name == @params);
            }
        }

        internal List<Directory> GetDirectories()
        {
            List<Directory> directories = new List<Directory>();
            directories.Add(this);
            foreach (Directory child in this.Children)
                if (!child.File)
                    directories.AddRange(child.GetDirectories());

            return directories;
        }

        internal int CalculateSize()
        {
            List<string> checkedDirectories = new List<string>();
            int totalSize = 0;

            if (this.Size < Program.MaxDirectorySize && !checkedDirectories.Contains(this.Parent.Name) && !this.File)
            {
                totalSize += this.Size;
                checkedDirectories.Add(this.Name);
            }
            
            foreach (var child in this.Children)
                totalSize += child.CalculateSize();

            return totalSize;
        }

        internal void CalculateSizeOfDirectories()
        {
            if (this.File)
                return;

            foreach (var child in this.Children)
            {
                child.CalculateSizeOfDirectories();
                this.Size += child.Size;
            }
        }

        internal void OutputTree()
        {
            Console.WriteLine(this);

            foreach (Directory dir in this.Children)
                dir.OutputTree();
        }

        public override string ToString()
        {
            string parentName = this.Parent == null ? "null" : this.Parent.Name;
            return $"{new string('\t', this.Depth)}{this.Name}, {this.Size}, {parentName}, {this.Children.Count}, {this.Depth}, {this.File} {this.Checked}";
        }
    }
}
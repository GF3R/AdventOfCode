using System.Text;

namespace AdventOfCode.Days;

public class Day7Solver
{
    private const int SystemSize = 70000000;
    private const int NeededSpace = 30000000;

    private static readonly FileSystemFolder Root = new("/")
    {
        Parent = null,
        Children = new List<FileSystemElement>(),
    };

    private FileSystemFolder _currentFolder = Root;

    public int Solve(string input, bool part1 = true)
    {
        var commandLines = input.Split(Environment.NewLine);

        foreach (var line in commandLines)
        {
            if (line.StartsWith("$"))
            {
                HandleCommand(line);
            }
            else
            {
                var fileInformation = line.Split(" ");
                if (fileInformation[0] == "dir")
                {
                    if (_currentFolder.Children.All(t => t.Name != fileInformation[1]))
                    {
                        _currentFolder.Children.Add(new FileSystemFolder(fileInformation[1])
                        {
                            Parent = _currentFolder,
                            Children = new List<FileSystemElement>()
                        });
                    }
                }
                else
                {
                    _currentFolder.Children.Add(new FileSystemElement(fileInformation[1])
                    {
                        Parent = _currentFolder,
                        Size = int.Parse(fileInformation[0])
                    });
                }
            }
        }
        Root.CalcuateSize();
        Console.Write(Root.ToLog());
        var folders = new List<FileSystemFolder>();

        if (part1)
        {
            GetAllFolders(Root, folders);
            return folders.Where(f => f.Size <= 100000).Sum(s => s.Size);
        }

        var remainingSpace = SystemSize - Root.Size;
        GetAllFolders(Root, folders);
        var orderedFolders = folders.OrderBy(t => t.Size);
        foreach (var folder in orderedFolders)
        {
            if (remainingSpace + folder.Size >= NeededSpace)
            {
                return folder.Size;
            }
        }

        return -1;
    }
    
    private void GetAllFolders(FileSystemFolder folder, IList<FileSystemFolder> folders)
    {
        folders.Add(folder);
        foreach (var child in folder.Children)
        {
            if (child is FileSystemFolder childFolder)
            {
                GetAllFolders(childFolder, folders);
            }
        }
    }

    private void HandleCommand(string command)
    {
        var commandParts = command.Split(' ');
        switch (commandParts[1])
        {
            case "cd":
                HandleCd(commandParts[2]);
                break;
            default:
                return;
        }
    }

    private void HandleCd(string path)
    {
        if (path == "/")
        {
            _currentFolder = Root;
            return;
        }

        if (path == "..")
        {
            _currentFolder = _currentFolder.Parent ?? throw new ArgumentNullException();
        }
        else
        {
            var subFolder = _currentFolder.Children.FirstOrDefault(x => x.Name == path && x is FileSystemFolder) as FileSystemFolder;
            if (subFolder == null)
            {
                subFolder = new FileSystemFolder(path)
                {
                    Parent = _currentFolder,
                    Children = new List<FileSystemElement>()
                };
                _currentFolder.Children.Add(subFolder);
            }

            _currentFolder = subFolder;
        }
    }
}

class FileSystemElement
{
    public FileSystemFolder? Parent { get; init; }

    public string Name { get; init; }

    public int Size { get; set; }
    
    public FileSystemElement(string name)
    {
        this.Name = name;
    }
    
    public virtual string ToLog(string prefix = "")
    {
        return $"{prefix}- {Name} (file, size={Size})\n";
    }
}

class FileSystemFolder : FileSystemElement
{
    public List<FileSystemElement> Children { get; set; } = new();

    public FileSystemFolder(string name) : base(name)
    {
    }
    
    public int CalcuateSize()
    {
        this.Size = this.Children.Sum(x => x is FileSystemFolder folder ? folder.CalcuateSize() : x.Size);
        return this.Size;
    }

    public override string ToLog(string prefix = "")
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append($"{prefix}- {Name} (dir, size={Size}))\n");
        
        foreach (var child in Children)
        {
            stringBuilder.Append(child.ToLog(prefix + " "));
        }
        
        return stringBuilder.ToString();
    }
}
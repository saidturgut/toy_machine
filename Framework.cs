namespace Toy_Machine;

public static class Framework
{
    public static int Accumulator;
    public static int Index;

    public static readonly Dictionary<string, int> MemorySlots = new();
    public static readonly Dictionary<string, int> Labels = new();

    private static readonly Type ReflectionsType = typeof(Reflections);
    private static readonly Reflections R = new();
    
    private static string[] _rawCommands = [];
    private static List<List<string>> _commands = [];
    private static List<string> _extracts = [];

    private static readonly string[] CommandDictionary =
        ["Get", "Add", "Sub", "Mul", "Div", "Mod", "Store", "Load", "Goto", "Ifzero", "Ifneg", "Ifpos", "Print", "Stop"];

    public static bool Terminate;
    
    public static void Main() => Init();

    private static void Init()
    {
        _rawCommands = File.ReadAllText("Example.txt").Split('\n').
            Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

        ProcessCommands();
        
        ComputeCommands();
    }

    public static void ProcessCommands()
    {
        _commands = [];
        
        foreach (string raw in _rawCommands)
        {
            _commands.Add(raw.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToList());
        }
        
        if(Correct(_commands[0][0]) != "Start") throw new Exception("Commands need to start with [start]");

        for (int i = 0; i < _commands.Count; i++)
        {
            if (Correct(_commands[i][0]) == "Stop")
            {
                ProcessVariables(i + 1);
                return;
            }
        }
        
        throw new Exception("Commands need to end with [stop]");
    }

    private static void ProcessVariables(int i)
    {
        if(_commands[i].Count >= 2 && _commands[i][1].All(char.IsDigit))
        {
            MemorySlots.TryAdd(_commands[i][0], int.Parse(_commands[i][1]));
            return;
        }
        
        throw new Exception($"Invalid syntax in {_commands[i]}");
    }
    
    private static void ComputeCommands()
    {
        _extracts = _commands[Index];
        
        ProcessLabels();
        
        Index++;
        
        if(_extracts.Count != 0) 
            ReflectionsType.GetMethod(Correct(_extracts[0]))?.Invoke(R, [_extracts.ElementAtOrDefault(1)]);
        
        if(!Terminate && Index != _rawCommands.Length) { ComputeCommands(); }
    }

    private static void ProcessLabels()
    {
        foreach (string frag in _extracts.ToList())
        {
            if (!MemorySlots.ContainsKey(frag) && !CommandDictionary.Contains(Correct(frag)))
            {
                for (int i = Index; i < _commands.Count; i++)
                {
                    if (_commands[i][0] == frag)
                    {
                        Labels.TryAdd(frag, i);
                    }
                }

                if(frag == _extracts[0]) _extracts.RemoveAt(0);
            }
        }
    }
    
    private static string Correct(string input)
    {
        if (input.Length == 0) return "";
        
        input = input.ToLower();
        
        return input[0].ToString().ToUpper() + input.Substring(1);
    }
}
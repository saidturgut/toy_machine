namespace Toy_Machine;

public static class Framework
{
    public static int Accumulator;
    public static int Index;

    public static readonly Dictionary<string, int> MemorySlots = new();
    public static Dictionary<string, int> Labels = new();

    private static readonly Type ReflectionsType = typeof(Reflections);
    private static readonly Reflections R = new();
    
    public static string[] Commands = [];

    private static readonly string[] CommandDictionary =
        ["Get", "Add", "Sub", "Mul", "Div", "Store", "Load", "Goto", "Ifzero", "Print", "Stop"];
    
    public static void Main()
    {        
        MemorySlots.Add("sum", 0);

        Commands = File.ReadAllText("Commands.csv").Split('\n').
            Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

        Compute();
    }

    public static void Compute()
    {
        List<string> extracts = Commands[Index].Split(' ').
            Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            
        foreach (string exp in extracts.ToList())
        {
            if (!MemorySlots.ContainsKey(exp) && !CommandDictionary.Contains(Fix(exp)))
            {
                Labels.TryAdd(exp, Index);

                if(exp == extracts[0]) extracts.RemoveAt(0);
            }
        }
            
        ReflectionsType.GetMethod(Fix(extracts[0]))?.Invoke(R, [extracts[^1]]);

        Index++;
        
        if(Index != Commands.Length) { Compute(); }
    }
    
    private static string Fix(string input)
    {
        if (input.Length == 0) return "";
        
        input = input.ToLower();
        
        return input[0].ToString().ToUpper() + input.Substring(1);
    }
}
    

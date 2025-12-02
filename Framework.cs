namespace Toy_Machine;

public static class Framework
{
    public static int Accumulator = 0;
    
    public static readonly MemorySlot[] MemorySlots = new MemorySlot[8];
    
    private static readonly string[] CommandDictionary =
        ["get", "add", "sub", "mul", "div", "store", "load", "ifzero", "print"];

    //public static List<Label> Labels = [new Label("start", 0)];
    
    public static void Main()
    {        
        Type type = typeof(Commands);
        Commands r = new Commands();
        
        MemorySlots[0] = new MemorySlot("sum", 0);
        List<Label> labels = [new Label("Start", 0)];

        string[] commands = File.ReadAllText("Commands.csv").Split('\n');
        
        for (int i = 0; i < commands.Length; i++)
        {
            if(commands[i].Trim() == "") continue;
            
            List<string> extracts = commands[i].Split(' ').ToList();

            if (i == 0 && Fix(extracts[0]) != labels[0].Name)
            {
                throw new Exception($"First label should be [{labels[0].Name}]");
            }
            
            foreach (string str in extracts.ToList())
            {
                if (str.Length != 0 && str[0] == ' ') extracts.Remove(str);
            }

            if (!CommandDictionary.Contains(Fix(extracts[0])))
            {
                if (Fix(extracts[0]) != labels[0].Name) labels.Add(new Label(extracts[0], i));
                
                extracts.RemoveAt(0);
            }
            
            switch (extracts.Count)
            {
                case 0:
                    continue;
                case 1:
                    if (CheckSyntax(extracts[0]))
                    {
                        type.GetMethod(Fix(extracts[0]))?.Invoke(r, null);
                        return;
                    }
                    
                    ThrowInvalidSyntax(commands[i]);
                    break;
                case 2:
                    if (!CheckSyntax(extracts[0]))
                    {
                        type.GetMethod(Fix(extracts[0]))?.Invoke(r, [extracts[1]]);
                        return;
                    }
                    
                    ThrowInvalidSyntax(commands[i]);
                    break;
                default:
                    ThrowInvalidSyntax(commands[i]);
                    break;
            }
        }
    }

    private static bool CheckSyntax(string input) => input == "get" || input == "print";
    
    private static void ThrowInvalidSyntax(string input) => throw new Exception($"Invalid syntax in [{input}]");
    
    private static string Fix(string input)
    {
        input = input.ToLower();
        
        return input[0].ToString().ToUpper() + input.Substring(1);
    }
}
    

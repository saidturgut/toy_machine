namespace Toy_Machine;

public static class Parser
{
    public static List<List<string>> ExtractWords(string filePath)
    {
        string[] raw = File.ReadAllText(filePath).Split('\n').
            Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

        List<List<string>> output = [];
        
        foreach (string str in raw)
        {
            output.Add(str.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToList());
        }

        return output;
    }

    public static List<string> CommandDictionary()
    {
        List<List<string>> extracts = ExtractWords("Reflections.cs");
        
        List<string> output = [];
        
        foreach (List<string> line in extracts)
        {
            for (int i = 0; i < line.Count; i++)
            {
                if (line[i] == "void")
                {
                    string[] str = line[i + 1].Split('(');
                    
                    output.Add(str[0]);
                }
            }
        }
        
        return output;
    }
}
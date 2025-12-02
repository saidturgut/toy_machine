namespace Toy_Machine;

public class MemorySlot
{
    public readonly string? Name;
    public int Value;

    public MemorySlot(){}

    public MemorySlot(string name, int value)
    {
        Name = name;
        Value = value;
    }
}

public class Label
{
    public readonly string Name = "";
    public int Index;
    
    public Label(){}
    
    public Label(string name, int index)
    {
        Name = name;
        Index = index;
    }
}
namespace Toy_Machine;

public class Commands
{
    // INPUTS

    public void Get() => Int32.TryParse(Console.ReadLine(), out Framework.Accumulator);
    
    // ARITHMETICS
    
    public void Add(string input) => Framework.Accumulator += ReturnValue(input);
    
    public void Sub(string input) => Framework.Accumulator -= ReturnValue(input);

    public void Mul(string input) => Framework.Accumulator *= ReturnValue(input);

    public void Div(string input) => Framework.Accumulator /= ReturnValue(input);
    
    // MEMORY PROCESSES

    public void Store(string name) => FindMemoryAddress(name).Value = Framework.Accumulator;

    public void Load(string name) => Framework.Accumulator = FindMemoryAddress(name).Value;
    
    // LABEL PROCESSES
    
    public void Goto(string input){}
    
    // CONDITIONALS
    
    public void Ifzero(int line)
    {
        if (Framework.Accumulator == 0)
        {
            //goto line
        }
    }
    
    // OUTPUTS

    public void Print() => Console.WriteLine(Framework.Accumulator);
    
    // STATICS

    private static int ReturnValue(string input)
    {
        if (input.All(char.IsDigit))
        {
            return int.Parse(input);
        }
        
        return FindMemoryAddress(input).Value;
    }
    
    private static MemorySlot FindMemoryAddress(string name)
    {
        foreach (MemorySlot mem in Framework.MemorySlots)
        {
            if (mem.Name == name)
            {
                return mem;
            }
        }

        throw new Exception($"Could not find memory address: {name}");
    }
}
namespace Toy_Machine;

public class Reflections
{
    // INPUTS

    public void Get(string arg) => Int32.TryParse(Console.ReadLine(), out Framework.Accumulator);
    
    // ARITHMETICS
    
    public void Add(string input) => Framework.Accumulator += ReturnValue(input);
    
    public void Sub(string input) => Framework.Accumulator -= ReturnValue(input);

    public void Mul(string input) => Framework.Accumulator *= ReturnValue(input);

    public void Div(string input) => Framework.Accumulator /= ReturnValue(input);
    
    // MEMORY PROCESSES

    public void Store(string name) => Framework.MemorySlots[name] = Framework.Accumulator;

    public void Load(string name) => Framework.Accumulator = Framework.MemorySlots[name];
    
    // LABEL PROCESSES
    
    public void Goto(string input) => Framework.Index = Framework.Labels[input];
    
    // CONDITIONALS
    
    public void Ifzero(string input)
    {
        if (Framework.Accumulator == 0)
        {
            Goto(input);
        }
    }
    
    // OUTPUTS

    public void Print(string arg) => Console.WriteLine(Framework.Accumulator);
    
    public void Stop(string arg) {}
    
    // STATICS

    private static int ReturnValue(string input)
    {
        if (input.All(char.IsDigit))
        {
            return int.Parse(input);
        }

        return Framework.MemorySlots[input];
    }
}
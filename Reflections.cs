using System.ComponentModel;

namespace Toy_Machine;

public class Reflections
{
    // INPUTS

    public void Get(string arg)
    {
        try
        {
            Framework.Accumulator = int.Parse(Console.ReadLine()!);
        }
        catch
        {
            throw new Exception("Input needs to be a number");
        }
    }
    
    // ARITHMETICS
    
    public void Add(string input) => Framework.Accumulator += ReturnValue(input);
    
    public void Sub(string input) => Framework.Accumulator -= ReturnValue(input);

    public void Mul(string input) => Framework.Accumulator *= ReturnValue(input);

    public void Div(string input) => Framework.Accumulator /= ReturnValue(input);
    
    public void Mod(string input) => Framework.Accumulator %= ReturnValue(input);
    
    // MEMORY

    public void Store(string name) => Framework.MemorySlots[name] = Framework.Accumulator;

    public void Load(string name) => Framework.Accumulator = Framework.MemorySlots[name];
    
    // RECURSION

    public void Goto(string input)
    {
        Framework.ProcessCommands();
        
        Framework.Index = Framework.Labels[input];
    }
    
    // CONDITIONALS
    
    public void Ifzero(string input)
    {
        if (Framework.Accumulator == 0)
        {
            Goto(input);
        }
    }
    
    public void Ifneg(string input)
    {
        if (Framework.Accumulator < 0)
        {
            Goto(input);
        }
    }

    public void Ifpos(string input)
    {
        if (Framework.Accumulator > 0)
        {
            Goto(input);
        }
    }
    
    // OUTPUTS

    public void Print(string arg) => Console.WriteLine(Framework.Accumulator);
    
    public void Stop(string arg) { Framework.Terminate = true; }
    
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
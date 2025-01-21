using System.Text;
using FileOP.Extensions;

namespace FileOP;

public static class Fiop
{
    private static readonly List<string> Arguments = ["move","copy","delete"];
    private static readonly List<string> Flags = ["target","destination","recursive","force"];
    
    public static void ParseArguments(string[] args)
    {
        string command = args[0];
        //TODO: Finishing parsing out the arguments that target and destination can take
        List<string> flags = args.Skip(1)
            .Where(s => s.StartsWith("--"))
            .Select(x => x.TrimSubstring("--"))
            .ToList();
        
        List<string> invalid_flags = [];
        
        if (!Arguments.Contains(command)) throw new ArgumentException(InvalidCommandPassed(command));
        foreach (string flag in flags)
        {
            // Add our flag to our list of invalid flags.
            // array[^1] just means from end or array[array.length - 1]
            if (!Flags.Contains(flag)) invalid_flags[^1] = flag;
            
            if (invalid_flags.Count is not 0) throw new ArgumentException(InvalidFlagsPassed(invalid_flags));

            // If we find any delete flags such as recursive or force
            // We want to throw an invalid argument exeption there as well
            List<string> delete_flags = flags.ToList().FindAll(s => s is ("recursive" or "force"));
            if (command != "delete" && delete_flags.Count != 0)
                throw new ArgumentException(InvalidDeleteFlags(delete_flags));
        }
    }
    
    private static string InvalidCommandPassed(string command)
    {
        StringBuilder sb = new();
        sb.AppendLine($"Invalid command {command}");
        sb.AppendLine($"Valid commands are: {string.Join(",", Arguments)}");
        return sb.ToString();
    }
    
    private static string InvalidFlagsPassed(List<string> flags)
    {
        StringBuilder sb = new();
        sb.AppendLine($"Invalid flags: {string.Join(",", Flags)}");
        sb.AppendLine($"Valid flags are: {string.Join(",", flags)}");
        return sb.ToString();
    }
    
     private static string InvalidDeleteFlags(List<string> flags)
     {
         StringBuilder sb = new();
         sb.AppendLine($"Invalid flags: {string.Join(",", Flags)}");
         sb.AppendLine($"Valid flags are: {string.Join(",", flags)}");
         return sb.ToString();
     }
}
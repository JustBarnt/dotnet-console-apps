using System.Text;
using FileOP.Extensions;

namespace FileOP;

public static class Fiop
{
    private static bool recursive = false;
    private static bool force = false;
    private static string target = "";
    private static string destination = "";

    private static readonly List<string> arguments = ["move","copy","delete"];
    private static readonly List<string> flags = ["target","destination","recursive","force"];

    public static void ParseArguments(string[] args)
    {
        //TODO: Finishing parsing out the arguments that target and destination can take
        string command = args[0];
        List<string> invalid_flags = [];
        List<string> flags = args.Skip(1)
            .Where(s => s.StartsWith("--"))
            .Select(x => x.TrimSubstring("--"))
            .ToList();

        if (!arguments.Contains(command)) throw new ArgumentException(InvalidCommandPassed(command));
        foreach (string flag in flags)
        {
            // Add our flag to our list of invalid flags.
            // array[^1] just means from end or array[array.length - 1]
            if (!Fiop.flags.Contains(flag)) invalid_flags[^1] = flag;

            if (invalid_flags.Count is not 0) throw new ArgumentException(InvalidFlagsPassed(invalid_flags));

            // If we find any delete flags such as recursive or force
            // and throw an invalid argument exception if command is not delete
            List<string> delete_flags = flags.ToList().FindAll(s => s is ("recursive" or "force"));
            if (command != "delete" && delete_flags.Count != 0)
                throw new ArgumentException(InvalidDeleteFlags(delete_flags));
        }
    }

    private static string InvalidCommandPassed(string command)
    {
        StringBuilder sb = new();
        sb.AppendLine($"Invalid command {command}");
        sb.AppendLine($"Valid commands are: {string.Join(",", arguments)}");
        return sb.ToString();
    }

    private static string InvalidFlagsPassed(List<string> flags)
    {
        StringBuilder sb = new();
        sb.AppendLine($"Invalid flags: {string.Join(",", Fiop.flags)}");
        sb.AppendLine($"Valid flags are: {string.Join(",", flags)}");
        return sb.ToString();
    }

     private static string InvalidDeleteFlags(List<string> flags)
     {
         StringBuilder sb = new();
         sb.AppendLine($"Invalid flags: {string.Join(",", Fiop.flags)}");
         sb.AppendLine($"Valid flags are: {string.Join(",", flags)}");
         return sb.ToString();
     }
}

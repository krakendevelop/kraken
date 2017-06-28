using System;
using System.Linq;

namespace TestConsole
{
  public class ConsoleCommand
  {
    public string Name { get; private set; }
    public string[] Params { get; private set; }

    public ConsoleCommand(string text)
    {
      var tokens = text.Split(new [] { ",", " " }, StringSplitOptions.RemoveEmptyEntries);
      if (tokens.Length == 0)
        return;

      Name = tokens[0].ToLowerInvariant();

      if (tokens.Length == 1)
        return;

      Params = tokens
        .Skip(1)
        .Select(a => a.TrimStart('-').ToLowerInvariant())
        .ToArray();
    }
  }
}
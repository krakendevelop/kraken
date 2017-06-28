using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Common.Exceptions;

namespace TestConsole
{
  public class ConsoleCommandProcessor
  {
    private readonly Dictionary<string, Func<string[], string>> _processorsByCmd;

    public ConsoleCommandProcessor()
    {
      _processorsByCmd = new Dictionary<string, Func<string[], string>>();
    }

    public ConsoleCommandProcessor AddProcessor(string cmd, Func<string[], string> func)
    {
      if (_processorsByCmd.ContainsKey(cmd))
        throw new KrakenException();

      _processorsByCmd.Add(cmd, func);
      return this;
    }

    public string Execute(string cmd, params string[] args)
    {
      return _processorsByCmd[cmd].Invoke(args);
    }
  }
}
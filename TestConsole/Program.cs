using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestConsole.Data;

namespace TestConsole
{
  class Program
  {
    static void Main(string[] args)
    {
      new DataTests().Test();

      Console.WriteLine("Finished");
      Console.ReadKey();
    }
  }
}

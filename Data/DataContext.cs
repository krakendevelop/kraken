using System;
using MySql.Data.MySqlClient;

namespace Data
{
  public class DataContext : IDisposable
  {
    private Command _command;
    private Connection _connection;

    public DataContext()
    {
      _command = new Command();
      _connection = new Connection();
    }

    public DataContext Query(string query)
    {
      _command.SetQuery(query);
      return this;
    }

    public DataContext SetParam(string param, object value)
    {
      _command.SetParam(param, value);
      return this;
    }

    public int Execute()
    {
      PreExecute();
      return _command.Execute();
    }

    internal T ExecuteReader<T>(Func<MySqlDataReader, T> readFunc)
    {
      PreExecute();
      return _command.ExecuteReader(readFunc);
    }

    private void PreExecute()
    {
      _connection.Open();
    }

    public void Dispose()
    {
      _command.Dispose();
      _connection.Dispose();
      _command = null;
      _connection = null;

      GC.SuppressFinalize(this);
    }
  }
}
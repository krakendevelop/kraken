using System;
using Common.Exceptions;
using MySql.Data.MySqlClient;

namespace Data
{
  internal class Command : IDisposable
  {
    private MySqlCommand _command;

    internal Command()
    {
      _command = new MySqlCommand();
    }

    internal void SetQuery(string query)
    {
      if (!string.IsNullOrEmpty(_command.CommandText))
        throw new KrakenException("Command text must be empty upon setting");

      _command.CommandText = query;
    }

    internal void SetParam(string name, object value)
    {
      _command.Parameters.AddWithValue(name, value);
    }

    internal int Execute()
    {
      return _command.ExecuteNonQuery();
    }

    internal T ExecuteReader<T>(Func<MySqlDataReader, T> readFunc)
    {
      var reader = _command.ExecuteReader();
      return readFunc(reader);
    }

    public void Dispose()
    {
      _command.Dispose();
      _command = null;

      GC.SuppressFinalize(this);
    }
  }
}
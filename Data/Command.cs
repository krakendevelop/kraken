using System;
using System.Data.SqlClient;
using Common.Exceptions;

namespace Data
{
  internal class Command : IDisposable
  {
    private SqlCommand _command;
    private Connection _connection;

    internal Command(Connection connection)
    {
      _connection = connection;
      _command = new SqlCommand
      {
        Connection = connection.SqlConnection
      };
    }

    internal void SetQuery(string query)
    {
      if (!string.IsNullOrEmpty(_command.CommandText))
        throw new KrakenException("Command text must be empty upon setting");

      _command.CommandText = query;
    }

    internal void SetParam(string name, object value)
    {
      _command.Parameters.AddWithValue(name, value ?? DBNull.Value);
    }

    internal int Execute()
    {
      return _command.ExecuteNonQuery();
    }

    internal T ExecuteReader<T>(Func<SqlDataReader, T> readFunc)
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
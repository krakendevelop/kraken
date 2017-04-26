using System;
using System.Data;
using Common.Exceptions;
using MySql.Data.MySqlClient;

namespace Data
{
  internal class Connection : IDisposable
  {
    private MySqlConnection _connection;

    internal Connection()
    {
      _connection = new MySqlConnection("");
    }

    internal void Open()
    {
      if (_connection.State == ConnectionState.Closed)
        _connection.Open();

      if (_connection.State != ConnectionState.Open)
        throw new KrakenException("Unable to open MySql connection");
    }

    public void Dispose()
    {
      _connection.Close();
      _connection.Dispose();
      _connection = null;

      GC.SuppressFinalize(this);
    }
  }
}
using System;
using System.Data;
using System.Data.SqlClient;
using Common.Exceptions;

namespace Data
{
  internal class Connection : IDisposable
  {
    public SqlConnection SqlConnection { get; private set; }

    internal Connection(string connString)
    {
      SqlConnection = new SqlConnection(connString);
    }

    internal void Open()
    {
      if (SqlConnection.State == ConnectionState.Closed)
        SqlConnection.Open();

      if (SqlConnection.State != ConnectionState.Open)
        throw new KrakenException("Unable to open connection");
    }

    public void Dispose()
    {
      SqlConnection.Close();
      SqlConnection.Dispose();
      SqlConnection = null;

      GC.SuppressFinalize(this);
    }
  }
}
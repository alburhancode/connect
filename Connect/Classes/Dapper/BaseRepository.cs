using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Connect.Classes.Dapper
{
	public abstract class BaseRepository : IDisposable
	{
		private bool _disposed;

		private IDbConnection _connection;

		public IDbConnection Connection()
		{
			const string key = "FQDryRun";
			return GetDbConnection(key);
		}

		private IDbConnection GetDbConnection(string key)
		{
			try
			{
				_connection = new SqlConnection(ConfigurationManager.ConnectionStrings[key].ConnectionString);
				_connection.Open();
			}
			catch (NullReferenceException ex)
			{
				if (_connection != null)
				{
					_connection.Dispose();
				}
				throw new Exception("Unable to connect to the database", ex);
			}
			return _connection;
		}

		public void FinaliseConnection(IDbConnection conn)
		{
			if (conn != null && conn.State != ConnectionState.Closed)
			{
				conn.Close();
			}
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (_disposed)
				return;

			if (disposing)
			{
				if (_connection != null)
				{
					_connection.Dispose();
				}
			}
			_disposed = true;
		}
	}
}
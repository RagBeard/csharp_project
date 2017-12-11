using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace TestData
{
	// interface to SQL Database

	public class DBConnection
	{
		string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\HistoricalEvents.mdf;Integrated Security=True";

		SqlConnection connection;
		SqlDataReader reader;

		public DBConnection()
		{

		}

		public void TransactAll(List<SqlCommand> commands)
		{			
			using (connection = new SqlConnection(connectionString))
			{

				connection.Open();

				var rows = 0;

				foreach (var com in commands)
				{
					com.Connection = connection;

					rows += com.ExecuteNonQuery();
				}

				Console.WriteLine("Performed Multiple queries, total rows affected: {0}", rows);
			}
		}


		//public void TransactAll(List<string> commands)
		//{
		//	SqlCommand cmd = new SqlCommand();
			
		//	// DO PARAMETERS @ 

		//	using (connection = new SqlConnection(connectionString))
		//	{

		//		connection.Open();

		//		var rows = 0;

		//		foreach (var com in commands)
		//		{
		//			cmd.CommandText = com;
		//			cmd.CommandType = CommandType.Text;
		//			cmd.CreateParameter();
		//			cmd.Connection = connection;
					
		//			rows += cmd.ExecuteNonQuery();
		//		}

		//		Console.WriteLine("Performed Multiple queries, total rows affected: ", rows);

		//	}
		//}

		public void Transact(string commandText)
		{
			SqlCommand cmd = new SqlCommand();

			using (connection = new SqlConnection(connectionString))
			{

				connection.Open();

				cmd.CommandText = commandText;
				cmd.CommandType = CommandType.Text;
				cmd.Connection = connection;


				var rows = cmd.ExecuteNonQuery();

				Console.WriteLine("Performing query: " + commandText);
				Console.WriteLine("DB Transaction, rows affected: {0}", rows);				
								
			}
		}

		public void Query(string commandText)
		{
			SqlCommand cmd = new SqlCommand();

			using (connection = new SqlConnection(connectionString))
			{
				cmd.CommandText = commandText;
				cmd.CommandType = CommandType.Text;
				cmd.Connection = connection;

				connection.Open();

				reader = cmd.ExecuteReader();

				if (reader.HasRows)
				{
					while (reader.Read())
					{
						Console.WriteLine("{0}\t{1}\t{2}\t{3}", reader.GetInt32(0), reader.GetDateTime(1), reader.GetString(2), reader.GetString(3));
					}
				}
				else
				{
					Console.WriteLine("No rows found.");
				}

				reader.Close();
			}
		}
	}
}

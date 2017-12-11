using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;



namespace TestData
{
	class Program
	{
		static string dataRoot = @"F:\Projects\GitRepos\csharp_project\datasets\";

		static string historyAD = "all_history_ad.json";
		static string historyADvalid = "all_history_ad_valid.json";

		static void Main(string[] args)
		{
			Console.WriteLine("Hello, Computer!");

			DBConnection dbc = new DBConnection();

			dbc.Query("SELECT * FROM Event;");


			HistoryLoader loader = new HistoryLoader(dataRoot + historyADvalid);

			loader.Load(dbc);



			Console.Read();
		}


	}
		

}

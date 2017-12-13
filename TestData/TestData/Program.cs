using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

using TestData.JSON;

namespace TestData
{
	class Program
	{
		static string dataRoot = @"F:\Projects\GitRepos\csharp_project\datasets\";
		
		static string historyADvalid = "all_history_ad_valid.json";
		static string jsonCities = "cities.json";
		static string jsonCountries = "country_codes.json";


		static void Main(string[] args)
		{
			Console.WriteLine("Hello, Computer!");

			DBConnection dbc = new DBConnection();

			dbc.Query("SELECT * FROM Event;");

			Processing proc = new Processing();


			List<Event> events = new List<Event>();
			List<City> cities = new List<City>();
			List<Country> countries = new List<Country>();

			Loader loader = new Loader();

			loader.LoadFromJSON(dataRoot + historyADvalid, out events);
			loader.LoadFromJSON(dataRoot + jsonCities, out cities);
			loader.LoadFromJSON(dataRoot + jsonCountries, out countries);
			
			proc.CountryCodesToCountryNames(cities, countries);
			

			DBInserter inserter = new DBInserter();
			//inserter.InsertCountriesInPlaces(countries, cities);
			inserter.ProcessEvents(cities, countries, events);

			//inserter.InsertEventsToDB(dbc, events);


			Console.Read();
		}


	}
		

}

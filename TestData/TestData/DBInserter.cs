using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestData.JSON;

namespace TestData
{
	public class DBInserter
	{

		public void InsertEventsToDB(DBConnection dbc, List<Event> events)
		{
			var commands = new List<SqlCommand>();

			foreach (var ev in events)
			{
				SqlCommand query = new SqlCommand("INSERT INTO Event VALUES (@date, @description)");

				query.Parameters.Add("@date", SqlDbType.Date).Value = ev.Date;
				query.Parameters.Add("@description", SqlDbType.NVarChar).Value = ev.Description;

				commands.Add(query);
			}

			dbc.TransactAll(commands);
		}

		public void InsertCountriesInPlaces(List<Country> countries, List<City> cities)
		{
			foreach (var country in countries)
			{
				City city = new City();
				city.Name = country.Name;
				city.Country = country.Name;

				cities.Add(city);
			}
		}

		public void InsertPlacesToDB(DBConnection dbc, List<City> cities, List<Country> countries)
		{
			InsertCountriesInPlaces(countries, cities);

			var commands = new List<SqlCommand>();

			foreach (var c in cities)
			{
				SqlCommand query = new SqlCommand("INSERT INTO Place VALUES (@name, @country, @lat, @lng)");
				
				//enforce NVarChar size restrictions. Sucks I know.
				var nameParam = query.Parameters.Add("@name", SqlDbType.NVarChar);
				nameParam.Value = c.Name;
				nameParam.Size = 50;

				var countryParam = query.Parameters.Add("@country", SqlDbType.NVarChar);
				countryParam.Value = c.Country;
				countryParam.Size = 50;
				
				query.Parameters.Add("@lat", SqlDbType.Float).Value = c.Lat;
				query.Parameters.Add("@lng", SqlDbType.Float).Value = c.Lng;
				

				commands.Add(query);
			}

			dbc.TransactAll(commands);
		}

		//Find Place info in the Event descriptions
		public void ProcessEvents(List<City> cities, List<Country> countries, List<Event> events)
		{
			foreach (var ev in events)
			{
				foreach (var country in countries)
				{
					string findString = country.Name;
					int idx = ev.Description.IndexOf(findString);

					if (idx >= 0)
					{
						//found city/country in description, break
						//Console.WriteLine("Found Country " + country.Name + " in Description '" + ev.Description + "'.");
						Console.Write(" " + country.Name);
						break;
					}
				}
			}


		}

		
		//foreach (var city in cities)
		//{
		//	string findString = city.Name + " ";
		//	int idx = ev.Description.IndexOf(findString);

		//	if (idx >= 0)
		//	{
		//		//found city/country in description, break
		//		Console.WriteLine("Found City " + city.Name + " in Description '" + ev.Description + "'.");
		//		break;
		//	}
		//}


	}
}

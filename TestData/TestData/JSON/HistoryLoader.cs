using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;


namespace TestData
{
	//reads stuff from JSON and populates SQL db
	class HistoryLoader
	{
		private class Event
		{
			public string Date { get; set; }
			public string Description { get; set; }
			public string Lang { get; set; }
			public string Category1 { get; set; }
			public string Category2 { get; set; }
			public string Category3 { get; set; }
			public string Granularity { get; set; }
		}

		List<Event> events = new List<Event>();


		private string source = @"F:\Projects\GitRepos\csharp_project\datasets\world_history_1880_test.json";


		public HistoryLoader(string jsonSource)
		{
			source = jsonSource;
		}
		
		public void Load(DBConnection dbc)
		{
			LoadFromJSON();
			//PushToDB(dbc);
		}

		//since the JSON of the "world events" dataset has duplicate keys... 
		private void LoadFromInvalidJSON()
		{
			using (StreamReader file = File.OpenText(source))
			{
				string json = file.ReadToEnd();

				using (var reader = new JsonTextReader(new StringReader(json)))
				{
					Event te = null;
					string prevPropertyName = "";

					while (reader.Read())
					{
						bool isString = reader.TokenType == JsonToken.String;
						bool isProperty = reader.TokenType == JsonToken.PropertyName;


						if (prevPropertyName == "event")
						{
							te = new Event();
						}

						if (isString && te != null)
						{
							string val = reader.Value.ToString();

							switch (prevPropertyName)
							{
								case "date":
									te.Date = val;
									break;

								case "description":
									te.Description = val;
									break;

								case "lang":
									te.Lang = val;
									break;

								case "category1":
									te.Category1 = val;
									break;

								case "granularity":
									te.Granularity = val;
									events.Add(te);
									te = null;
									break;

								default:
									break;
							}

						}

						if (isProperty)
						{
							prevPropertyName = reader.Value.ToString();
						}
						
					}
				}
			}
		}

		//this works for valid JSON
		private void LoadFromJSON()
		{
			using (StreamReader file = File.OpenText(source))
			{
				string json = file.ReadToEnd();

				events = JsonConvert.DeserializeObject < List < Event >> (json);
			}
		}

		
		//private void OpenJSONToList(Type t, ref List<Type> list)
		//{
		//	using (StreamReader file = File.OpenText(source))
		//	{
		//		string json = file.ReadToEnd();

		//		events = JsonConvert.DeserializeObject<List<Event>>(json);
		//	}
		//}

		// hard-coded to insert into Event table
		private void PushToDB(DBConnection dbc)
		{
			var commands = new List<SqlCommand>();
			
			
			foreach (var ev in events)
			{
				SqlCommand query = new SqlCommand("INSERT INTO Event VALUES (@date, @description, @location)");

				//query.Parameters.AddWithValue("@table", "Event");
				query.Parameters.AddWithValue("@date", ev.Date);
				query.Parameters.AddWithValue("@description", ev.Description);
				query.Parameters.AddWithValue("@location", "");
								
				commands.Add(query);
			}


			dbc.TransactAll(commands);
		}



	}
}

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CSharpRestClient
{
	class MainClass
	{
		static HttpClient client = new HttpClient();

		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			RunAsync().Wait();
		}


		static async Task RunAsync()
		{
			client.BaseAddress = new Uri("http://localhost:8080/artists");
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			
			//create
			Artist artist = new Artist(12356, "descend into despair", "brasov, centrul vechi", "10-10-2021", 10000);
			Console.WriteLine("Creating artist {0}", artist);
			Console.WriteLine(await PostArtistAsync("http://localhost:8080/artists", artist));
			Console.ReadLine();

			// get one by id
			int id = 1;
			Console.WriteLine("Get artist {0}", id);
			Artist result = await GetArtistAsync("http://localhost:8080/artists/" + id);
			Console.WriteLine("Am primit {0}", result);
			Console.ReadLine();
			
			// get all
			Console.WriteLine("Get all before delete");
			Artist[] all = await GetAllAsync("http://localhost:8080/artists/");
			Console.WriteLine("Am primit {0} artisti", all.Length);
			foreach (var each in all)
			{
				Console.WriteLine(each);
			}
			Console.ReadLine();
			
			// update
			artist.AvailableTicketsNumber += 100;
			Console.WriteLine("Updating artist");
			Console.WriteLine(await PutArtistAsync("http://localhost:8080/artists", artist.Id, artist));
			Console.ReadLine();
			
			
			// get one by id after update
			Console.WriteLine("Get artist after update");
			result = await GetArtistAsync("http://localhost:8080/artists/" + artist.Id);
			Console.WriteLine("Am primit {0}", result);
			Console.ReadLine();
			
			
			// delete
			Console.WriteLine("Deleting artist {0}", artist);
			Console.WriteLine(await DeleteArtistAsync("http://localhost:8080/artists", 12356));
			Console.ReadLine();


			// get all
			Console.WriteLine("Get all after delete");
			all = await GetAllAsync("http://localhost:8080/artists/");
			Console.WriteLine("Am primit {0} artisti", all.Length);
			foreach (var each in all)
			{
				Console.WriteLine(each);
			}
			Console.ReadLine();
		}

		private static async Task<object> PutArtistAsync(string path, int id, Artist artist)
		{
			object product = null;
			var json = JsonConvert.SerializeObject(artist);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await client.PutAsync(path + "/" + id, content);
			if (response.IsSuccessStatusCode)
			{
				product = await response.Content.ReadAsStringAsync();
			}
			return product;
		}

		private static async Task<object> PostArtistAsync(string path, Artist artist)
		{
			object product = null;
			var json = JsonConvert.SerializeObject(artist);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await client.PostAsync(path, content);
         			
			if (response.IsSuccessStatusCode)
			{
				product = await response.Content.ReadAsStringAsync();
			}
			return product;
		}
		
		private static async Task<object> DeleteArtistAsync(string path, int id)
		{
			object product = null;
			var response = await client.DeleteAsync(path + "/" + id);
         			
			if (response.IsSuccessStatusCode)
			{
				product = await response.Content.ReadAsStringAsync();
			}
			return product;
		}

		static async Task<String> GetTextAsync(string path)
		{
			String product = null;
			HttpResponseMessage response = await client.GetAsync(path);
			if (response.IsSuccessStatusCode)
			{
				product = await response.Content.ReadAsStringAsync();
			}
			return product;
		}


		static async Task<Artist> GetArtistAsync(string path)
		{
			Artist a = null;
			HttpResponseMessage response = await client.GetAsync(path);
			if (response.IsSuccessStatusCode)
			{
				a = await response.Content.ReadAsAsync<Artist>();
			}
			return a;
		}
		
		static async Task<Artist[]> GetAllAsync(string path)
		{
			Artist[] a = null;
			HttpResponseMessage response = await client.GetAsync(path);
			if (response.IsSuccessStatusCode)
			{
				a = await response.Content.ReadAsAsync<Artist[]>();
			}
			return a;
		}

	}
	public class Artist
	{
		[JsonProperty("id")]
		public int Id { get; set; }
		
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("location")]
		public string Location { get; set; }
		
		[JsonProperty("date")]
		public string Date { get; set; }
		
		[JsonProperty("availableTicketsNumber")]
		public Int32 AvailableTicketsNumber { get; set; }

		public Artist(int id, string name, string location, string date, int availableTicketsNumber)
		{
			Id = id;
			Name = name;
			Location = location;
			Date = date;
			AvailableTicketsNumber = availableTicketsNumber;
		}

		public override string ToString()
		{
			return "Artist { Id = " + Id + ", Name = " + Name + ", Location = " + Location + ", Date = " + Date +
			       ", AvailableTicketsNumber = " + AvailableTicketsNumber + "}";
		}
	}
	
}


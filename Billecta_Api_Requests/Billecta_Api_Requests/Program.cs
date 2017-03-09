using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// ReSharper disable InconsistentNaming

namespace Billecta_Api_Requests
{
	class Program
	{
		private static string clientId;
		private static string clientSecret;
		static readonly HttpClient client = new HttpClient();
		private static byte[] byteArray;

		static void Main(string[] args)
		{
			var appSettings = ConfigurationManager.AppSettings;
			clientId = appSettings["Billecta.ClientID"];
			clientSecret = appSettings["Billecta.ClientSecret"];
			byteArray = Encoding.ASCII.GetBytes(clientId + ":" + clientSecret);
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

			Get_AllCreditor().Wait();

		}


		public static async Task Get_Currencies()
		{
			//var appSettings = ConfigurationManager.AppSettings;
			//clientId = appSettings["Billecta.ClientID"];
			//clientSecret = appSettings["Billecta.ClientSecret"];
			//byteArray = Encoding.ASCII.GetBytes(clientId + ":" + clientSecret);
			//client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

			HttpResponseMessage response = await client.GetAsync("https://apitest.billecta.com/v1/" + "currencies/currencies");
			response.EnsureSuccessStatusCode();
			var responseBody = await response.Content.ReadAsStringAsync();

			Console.WriteLine(responseBody);
			Console.ReadLine();
		}



		public static async Task Get_AllCreditor()
		{
			//var appSettings = ConfigurationManager.AppSettings;
			//clientId = appSettings["Billecta.ClientID"];
			//clientSecret = appSettings["Billecta.ClientSecret"];
			//byteArray = Encoding.ASCII.GetBytes(clientId + ":" + clientSecret);
			//client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

			HttpResponseMessage response = await client.GetAsync("https://apitest.billecta.com/v1/" + "creditors/creditors");
			response.EnsureSuccessStatusCode();
			var responseBody = await response.Content.ReadAsStringAsync();
			JObject o = JObject.Parse(responseBody);
			Console.WriteLine(o);

			var s = JsonConvert.DeserializeObject<CreditorPublicId>(responseBody);

			Console.ReadLine();
		}


		public static async Task Get_Authentication_Claims()
		{
			var appSettings = ConfigurationManager.AppSettings;
			clientId = appSettings["Billecta.ClientID"];
			clientSecret = appSettings["Billecta.ClientSecret"];
			byteArray = Encoding.ASCII.GetBytes(clientId + ":" + clientSecret);
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

			HttpResponseMessage response = await client.GetAsync("https://apitest.billecta.com/v1/" + "authentication/apiauthenticate");
			response.EnsureSuccessStatusCode();
			var responseBody = await response.Content.ReadAsStringAsync();

			Console.WriteLine(responseBody);
			Console.ReadLine();
		}


		public static async Task Delete_Products()
		{
			var appSettings = ConfigurationManager.AppSettings;
			clientId = appSettings["Billecta.ClientID"];
			clientSecret = appSettings["Billecta.ClientSecret"];
			byteArray = Encoding.ASCII.GetBytes(clientId + ":" + clientSecret);
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

			HttpResponseMessage response = await client.DeleteAsync("https://apitest.billecta.com/v1/" + "products/products/2");
			response.EnsureSuccessStatusCode();
			var responseBody = await response.Content.ReadAsStringAsync();

			Console.WriteLine(responseBody);
			Console.ReadLine();
		}



		public static async Task Get_Product()
		{
			var appSettings = ConfigurationManager.AppSettings;
			clientId = appSettings["Billecta.ClientID"];
			clientSecret = appSettings["Billecta.ClientSecret"];
			byteArray = Encoding.ASCII.GetBytes(clientId + ":" + clientSecret);
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

			HttpResponseMessage response = await client.GetAsync("https://apitest.billecta.com/v1/" + "products/products/{id}?productid={productid}");
			response.EnsureSuccessStatusCode();
			var responseBody = await response.Content.ReadAsStringAsync();

			Console.WriteLine(responseBody);
			Console.ReadLine();
		}




		public static async Task Create_Products()
		{
			var appSettings = ConfigurationManager.AppSettings;
			clientId = appSettings["Billecta.ClientID"];
			clientSecret = appSettings["Billecta.ClientSecret"];
			byteArray = Encoding.ASCII.GetBytes(clientId + ":" + clientSecret);
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));


			var data = new
			{
				root = new RootObject()
				{
					type = "proTy",
					title = "ProRootTitle",
					properties = new Properties()
					{
						CreditorPublicId = new CreditorPublicId()
						{
							type = "Creadior ID 1"
						},
						Description = new Description()
						{
							type = new List<string>() { "Test description 1", "Test des 2" }
						},
						ProductPublicId = new ProductPublicId()
						{
							type = "Product  ID 1"
						},
						UnitPrice = new UnitPrice()
						{
							type = "2500"
						},
						VAT = new VAT()
						{
							type = "3948938493893"
						},
						ProductType = new ProductType() { type = "pro type" },
						Units = new Units() { type = new List<string>() { "1" } },
						IsActive = new IsActive() { type = "yes" },
						ProductExternalId = new ProductExternalId() { type = new List<string>() { "ex ID" } }

					}
				}
			};

			HttpResponseMessage response = await client.PostAsJsonAsync("https://apitest.billecta.com/v1/products/products", data);

			response.EnsureSuccessStatusCode();
			var responseBody = await response.Content.ReadAsStringAsync();

			Console.WriteLine(responseBody);
			Console.ReadLine();
		}
	}

	public class Creditor
	{
		public string CreditorPublicId { get; set; }
	}





	public class ProductPublicId
	{
		public string type { get; set; }
	}

	public class CreditorPublicId
	{
		public string type { get; set; }
	}

	public class ArticleNumber
	{
		public List<string> type { get; set; }
	}

	public class ProductExternalId
	{
		public List<string> type { get; set; }
	}

	public class Description
	{
		public List<string> type { get; set; }
	}

	public class Units
	{
		public List<string> type { get; set; }
	}

	public class IsActive
	{
		public string type { get; set; }
	}

	public class UnitPrice
	{
		public string type { get; set; }
	}

	public class VAT
	{
		public string type { get; set; }
	}

	public class BookKeepingAccount
	{
		public List<string> type { get; set; }
	}

	public class BookKeepingSalesEUAccount
	{
		public List<string> type { get; set; }
	}

	public class BookKeepingSalesEUVATAccount
	{
		public List<string> type { get; set; }
	}

	public class BookKeepingSalesNonEUAccount
	{
		public List<string> type { get; set; }
	}

	public class BookKeepingPurchaseAccount
	{
		public List<string> type { get; set; }
	}

	public class ProductType
	{
		public string type { get; set; }
		public List<int> @enum { get; set; }
	}

	public class Properties
	{
		public ProductPublicId ProductPublicId { get; set; }
		public CreditorPublicId CreditorPublicId { get; set; }
		public ArticleNumber ArticleNumber { get; set; }
		public ProductExternalId ProductExternalId { get; set; }
		public Description Description { get; set; }
		public Units Units { get; set; }
		public IsActive IsActive { get; set; }
		public UnitPrice UnitPrice { get; set; }
		public VAT VAT { get; set; }
		public BookKeepingAccount BookKeepingAccount { get; set; }
		public BookKeepingSalesEUAccount BookKeepingSalesEUAccount { get; set; }
		public BookKeepingSalesEUVATAccount BookKeepingSalesEUVATAccount { get; set; }
		public BookKeepingSalesNonEUAccount BookKeepingSalesNonEUAccount { get; set; }
		public BookKeepingPurchaseAccount BookKeepingPurchaseAccount { get; set; }
		public ProductType ProductType { get; set; }
	}

	public class RootObject
	{
		public string title { get; set; }
		public string type { get; set; }
		public Properties properties { get; set; }
	}
}

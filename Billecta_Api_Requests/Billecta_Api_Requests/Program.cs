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
using static System.Console;
// ReSharper disable InconsistentNaming

namespace Billecta_Api_Requests
{
	public class Program
	{
		static readonly HttpClient Client = new HttpClient();

		private static string BaseUrl;
		public static string crediotPublicId;
		public static string productPublicId;


		public static void Main(string[] args)
		{
			var appSettings = ConfigurationManager.AppSettings;
			var ClientId = appSettings["Billecta.ClientID"];
			var ClientSecret = appSettings["Billecta.ClientSecret"];
			var ByteArray = Encoding.ASCII.GetBytes(ClientId + ":" + ClientSecret);
			Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(ByteArray));
			BaseUrl = "https://apitest.billecta.com/v1/";

			crediotPublicId = "bfa37953-e5c3-4edd-b0f2-da0c235d07da"; // requesting to secure authentice response
			productPublicId = "8c58cc36-2b80-4083-a733-bf069cf8fca1"; // from list of products choosen product 1

			Create_Products().Wait();
		}


		public static async Task Get_Currencies()
		{
			HttpResponseMessage response = await Client.GetAsync("https://apitest.billecta.com/v1/" + "currencies/currencies");
			response.EnsureSuccessStatusCode();
			var responseBody = await response.Content.ReadAsStringAsync();

			WriteLine(responseBody);
			ReadLine();
		}


		public static async Task Get_AllCreditor()
		{
			HttpResponseMessage response = await Client.GetAsync("https://apitest.billecta.com/v1/" + "creditors/creditors");
			response.EnsureSuccessStatusCode();
			var responseBody = await response.Content.ReadAsStringAsync();
			var creditors = (List<Creditor>)JsonConvert.DeserializeObject(responseBody, typeof(List<Creditor>));

			var count = 0;
			WriteLine("Retrieving list of all 'Creditors'....\n");
			foreach (var creditor in creditors)
			{
				count++;
				WriteLine("#" + count);
				WriteLine("CreditorPublicId" + " : " + creditor.CreditorPublicId);
				WriteLine("OrgNo" + " : " + creditor.OrgNo);

				foreach (var pair in creditor.CreditorDetails)
				{
					WriteLine(pair.Key + " : " + pair.Value);
				}
				WriteLine("\n \n");
			}

			WriteLine("Finish !");
			ReadLine();
		}


		public static async Task Get_Authentication_Claims()
		{
			HttpResponseMessage response = await Client.GetAsync("https://apitest.billecta.com/v1/" + "authentication/apiauthenticate");
			response.EnsureSuccessStatusCode();
			var responseBody = await response.Content.ReadAsStringAsync();

			WriteLine(responseBody);
			ReadLine();
		}


		public static async Task Get_Product()
		{
			HttpResponseMessage response = await Client.GetAsync(BaseUrl + "products/products/" + crediotPublicId + "?productid=" + productPublicId);
			response.EnsureSuccessStatusCode();
			var responseBody = await response.Content.ReadAsStringAsync();
			var product = JsonConvert.DeserializeObject<Product>(responseBody);

			WriteLine("Retrieving a specific product....\n");
			WriteLine("CreditorPublicId" + " : " + product.CreditorPublicId);
			WriteLine("ProductExternalId" + " : " + product.ProductExternalId);
			WriteLine("ProductPublicId:" + " : " + product.ProductPublicId);
			foreach (var pair in product.ProductetailContainer)
			{
				WriteLine(pair.Key + " : " + pair.Value);
			}
			WriteLine("\nFinish !");
			ReadLine();
		}


		public static async Task GetAll_Product()
		{
			HttpResponseMessage response = await Client.GetAsync(BaseUrl + "products/products/" + crediotPublicId);
			response.EnsureSuccessStatusCode();
			var responseBody = await response.Content.ReadAsStringAsync();
			var products = (List<Product>)JsonConvert.DeserializeObject(responseBody, typeof(List<Product>));

			var count = 0;

			WriteLine("Retrieving list of all products....\n");

			foreach (var product in products)
			{
				count++;
				WriteLine("#" + count);
				WriteLine("CreditorPublicId" + " : " + product.CreditorPublicId);
				WriteLine("ProductExternalId" + " : " + product.ProductExternalId);
				WriteLine("ProductPublicId:" + " : " + product.ProductPublicId);

				foreach (var pair in product.ProductetailContainer)
				{
					WriteLine(pair.Key + " : " + pair.Value);
				}

				WriteLine("\n \n");
			}

			WriteLine("Finish !");
			ReadLine();
		}

		//nt done
		public static async Task UpdateProduct()
		{
			var data = new
			{
				CreditorPublicId = crediotPublicId,
				ProductPublicId = productPublicId,
				ProductExternalId = "2222"
			};

			HttpResponseMessage response = await Client.PutAsJsonAsync(BaseUrl + "products/products/", data);
			response.EnsureSuccessStatusCode();
			var responseBody = await response.Content.ReadAsStringAsync();
			var product = JsonConvert.DeserializeObject<Product>(responseBody);


		}


		//public static async Task Delete_Products()
		//{
		//	//var appSettings = ConfigurationManager.AppSettings;
		//	//ClientId = appSettings["Billecta.ClientID"];
		//	//ClientSecret = appSettings["Billecta.ClientSecret"];
		//	//ByteArray = Encoding.ASCII.GetBytes(ClientId + ":" + ClientSecret);
		//	//Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(ByteArray));

		//	HttpResponseMessage response = await Client.DeleteAsync("https://apitest.billecta.com/v1/" + "products/products/2");
		//	response.EnsureSuccessStatusCode();
		//	var responseBody = await response.Content.ReadAsStringAsync();

		//	WriteLine(responseBody);
		//	ReadLine();
		//}


		//OK

		//nt done

		public static async Task Create_Products()
		{
			var data = new
			{
				ProductPublicId = "847875384759384hf99",
				CreditorPublicId = crediotPublicId,
				Description = "some des",
				IsActive = true,
				UnitPrice = 33,
				VAT = 25,
				ProductTypeTypeView = 0
			};

			HttpResponseMessage response = await Client.PostAsJsonAsync("https://apitest.billecta.com/v1/products/products", data);

			response.EnsureSuccessStatusCode();
			var responseBody = await response.Content.ReadAsStringAsync();

			WriteLine(responseBody);
			ReadLine();
		}
	}
}

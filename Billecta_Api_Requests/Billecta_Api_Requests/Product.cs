using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Billecta_Api_Requests
{
	public class Product
	{
		public string ProductPublicId { get; set; }

		public string CreditorPublicId { get; set; }

		public string ProductExternalId { get; set; }

		[JsonExtensionData]
		public IDictionary<string, JToken> ProductetailContainer { get; set; }
	}
}
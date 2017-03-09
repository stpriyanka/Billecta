using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Billecta_Api_Requests
{
	public class Creditor
	{
		public string CreditorPublicId { get; set; }

		public string OrgNo { get; set; }

		[JsonExtensionData]
		public IDictionary<string, JToken> CreditorDetails { get; set; }

	}
}
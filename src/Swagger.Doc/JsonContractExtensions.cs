
/******************************************************
* author :  jilonglv
* email  : jilonglv@gmail.com
* function:
* history:  created by jilonglv 2020/6/12 19:13:02
* clrversion :4.0.30319.42000
******************************************************/

namespace Swagger.Doc
{
    using Newtonsoft.Json.Serialization;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

	public static class JsonContractExtensions
	{
		private static IEnumerable<string> HttpTypeNames = new string[]
		{
			"System.Net.Http.HttpRequestMessage",
			"System.Net.Http.HttpResponseMessage",
			"System.Web.Http.IHttpActionResult"
		};

		public static bool IsSelfReferencing(this JsonDictionaryContract dictionaryContract)
		{
			return dictionaryContract.UnderlyingType == dictionaryContract.DictionaryValueType;
		}

		public static bool IsSelfReferencing(this JsonArrayContract arrayContract)
		{
			return arrayContract.UnderlyingType == arrayContract.CollectionItemType;
		}

		public static bool IsInferrable(this JsonObjectContract objectContract)
		{
			return !JsonContractExtensions.HttpTypeNames.Contains(objectContract.UnderlyingType.FullName);
		}
	}
}

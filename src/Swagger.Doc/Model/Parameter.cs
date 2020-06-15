
/******************************************************
* author :  jilonglv
* email  : jilonglv@gmail.com
* function:
* history:  created by jilonglv 2020/6/12 16:17:33
* clrversion :4.0.30319.42000
******************************************************/

namespace Swagger.Doc
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Text;

	public class Parameter : PartialSchema
	{
		[JsonProperty("$ref")]
		public string @ref;

		public string name;

		public string @in;

		public string description;

		public bool? required;

		public Schema schema;

		public Dictionary<string, object> vendorExtensions = new Dictionary<string, object>();
	}
}

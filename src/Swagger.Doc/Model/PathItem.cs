
/******************************************************
* author :  jilonglv
* email  : jilonglv@gmail.com
* function:
* history:  created by jilonglv 2020/6/12 16:03:33
* clrversion :4.0.30319.42000
******************************************************/

namespace Swagger.Doc
{
	using Newtonsoft.Json;
	using System;
	using System.Collections.Generic;
	using System.Text;
	public class PathItem
	{
		[JsonProperty("$ref")]
		public string @ref;

		public Operation get;

		public Operation put;

		public Operation post;

		public Operation delete;

		public Operation options;

		public Operation head;

		public Operation patch;

		public IList<Parameter> parameters;

		public Dictionary<string, object> vendorExtensions = new Dictionary<string, object>();
	}
}

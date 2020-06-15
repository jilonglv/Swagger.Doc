
/******************************************************
* author :  jilonglv
* email  : jilonglv@gmail.com
* function:
* history:  created by jilonglv 2020/6/12 16:19:14
* clrversion :4.0.30319.42000
******************************************************/

namespace Swagger.Doc
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Text;
	public class Schema
	{
		[JsonProperty("$ref")]
		public string @ref;

		public string format;

		public string title;

		public string description;

		public object @default;

		public int? multipleOf;

		public int? maximum;

		public bool? exclusiveMaximum;

		public int? minimum;

		public bool? exclusiveMinimum;

		public int? maxLength;

		public int? minLength;

		public string pattern;

		public int? maxItems;

		public int? minItems;

		public bool? uniqueItems;

		public int? maxProperties;

		public int? minProperties;

		public IList<string> required;

		public IList<object> @enum;

		public string type;

		public Schema items;

		public IList<Schema> allOf;

		public IDictionary<string, Schema> properties;

		public Schema additionalProperties;

		public string discriminator;

		public bool? readOnly;

		public Xml xml;

		public ExternalDocs externalDocs;

		public object example;

		public Dictionary<string, object> vendorExtensions = new Dictionary<string, object>();
	}
}

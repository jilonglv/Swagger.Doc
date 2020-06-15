
/******************************************************
* author :  jilonglv
* email  : jilonglv@gmail.com
* function:
* history:  created by jilonglv 2020/6/12 16:21:13
* clrversion :4.0.30319.42000
******************************************************/

namespace Swagger.Doc
{
    using System;
    using System.Collections.Generic;
    using System.Text;

	public class SecurityScheme
	{
		public string type;

		public string description;

		public string name;

		public string @in;

		public string flow;

		public string authorizationUrl;

		public string tokenUrl;

		public IDictionary<string, string> scopes;

		public Dictionary<string, object> vendorExtensions = new Dictionary<string, object>();
	}
}
